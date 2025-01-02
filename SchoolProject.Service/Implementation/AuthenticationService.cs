using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Data.Responses;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Service.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolProject.Service.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _context;
        private readonly IUrlHelper _urlHelper;
        private readonly IEncryptionService _encryptionService;

        //private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshToken;

        #endregion

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings,
            IRefreshTokenRepository refreshTokenRepository,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService,
            ApplicationDbContext context,
            IUrlHelper urlHelper,
            IEncryptionService encryptionService
            )
        {
            _jwtSettings = jwtSettings;
            _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _emailService = emailService;
            _encryptionService = encryptionService;
            _urlHelper = urlHelper;
            //_userRefreshToken = new ConcurrentDictionary<string, RefreshToken>();
        }

        #endregion

        #region Handel Functions
        public async Task<JwtAuthResponse> GetJWTToken(User user)
        {


            var (token, accessToken) = await GenerateJwtToken(user);

            var refreshToken = GetRefreshToken(user.UserName);

            var userRefreshToken = new UserRefreshToken
            {
                CreationTime = DateTime.Now,
                ExpireDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserId = user.Id,
                IsUsed = true,
                IsRevoked = false,
                JwtId = token.Id,
                RefreshToken = refreshToken.TokenString,
                Token = accessToken,
            };
            await _refreshTokenRepository.AddAsync(userRefreshToken);

            var response = new JwtAuthResponse()
            {
                RefreshToken = refreshToken,
                AccessToken = accessToken,
            };
            return response;
        }
        private async Task<List<Claim>> GetClaimsAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var userClaims = await _userManager.GetClaimsAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
                new Claim(nameof(UserClaimModel.UserId), user.Id)
            };
            claims.AddRange(userClaims);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        private RefreshToken GetRefreshToken(string userName)
        {
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName = userName,
                TokenString = GenerateRefreshToken()
            };


            //  _userRefreshToken.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<JwtAuthResponse> GetRefreshToken(User user, string accessToken, string refreshToken)
        {

            //Generate RefreshToken

            var (jwtSecurityToken, newToken) = await GenerateJwtToken(user);
            var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking()
              .FirstOrDefaultAsync(x => x.Token == accessToken &&
              x.RefreshToken == refreshToken &&
              x.UserId == user.Id);
            var response = new JwtAuthResponse();
            response.AccessToken = newToken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = user.UserName;
            refreshTokenResult.TokenString = refreshToken;
            refreshTokenResult.ExpireAt = userRefreshToken.ExpireDate;
            response.RefreshToken = refreshTokenResult;
            return response;

        }
        public JwtSecurityToken ReadJwtToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new ArgumentNullException(nameof(accessToken));

            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }
        private async Task<(JwtSecurityToken, string)> GenerateJwtToken(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = await GetClaimsAsync(user);
            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(
                  new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                  SecurityAlgorithms.HmacSha256Signature
                )
            );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return (token, accessToken);

        }
        public async Task<string> ValidateToken(string accessToken)
        {
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifetime,
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret))
            };
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

                if (validator == null)
                {
                    return "InvalidToken";
                }

                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> ValidateDetails(JwtSecurityToken token, string accessToken, string refreshToken)
        {
            //Validation Token, RefreshToken
            if (token == null || !token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return "Algorithm Is Wrong";
            }
            if (token.ValidTo > DateTime.UtcNow)
            {
                return "Token Is Not Expired";
            }
            //Get User 
            var userId = token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserId)).Value;

            var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking()
                .FirstOrDefaultAsync(x => x.Token == accessToken &&
                x.RefreshToken == refreshToken &&
                x.UserId == userId);

            if (userRefreshToken == null)
            {
                return "Refresh Token Is Not Found";
            }
            if (userRefreshToken.ExpireDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(userRefreshToken);
                return "Refresh Token Is Not Expired";
            }
            return "Valid";
        }

        public async Task<string> RegisterUserAsync(User user, string password)
        {

            var trans = await _context.Database.BeginTransactionAsync();
            try
            {
                // register user 
                var result = await _userManager.CreateAsync(user, password);
                //return response
                if (!result.Succeeded)
                {
                    return string.Join("-", result.Errors.Select(x => x.Description));
                }
                await _userManager.AddToRoleAsync(user, "User");
                // Send Confirm Email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var requestAccessor = _httpContextAccessor.HttpContext.Request;
                //var returnUrl = $"{requestAccessor.Scheme}://{requestAccessor.Host}/Api/V1/Authentication/ConfirmEmail?userId={user.Id}&code={code}";

                var returnUrl = $"{requestAccessor.Scheme}://{requestAccessor.Host}{_urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code })}";

                //message 
                var sendEmailResult = await _emailService.SendEmailAsync(user.Email, "Confirm Your Email", returnUrl);
                if (sendEmailResult != "Success")
                    return "FailedToSendEmail";
                else
                    await trans.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                await trans.RollbackAsync();
                return "FailedToAddUser";
            }

        }

        public async Task<string> ConfirmEmail(string useIdr, string code)
        {
            var user = await _userManager.FindByIdAsync(useIdr);
            var confirm = await _userManager.ConfirmEmailAsync(user, code);
            if (!confirm.Succeeded)
            {
                return "Error";
            }
            return "Success";
        }

        public async Task<string> SendResetPasswordCode(string email)
        {
            var trans = await _context.Database.BeginTransactionAsync();
            try
            {

                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                    return "UserNotFound";
                Random random = new Random();
                string randomNumber = random.Next(0, 100000).ToString("D6");
                //Encrypt
                user.Code = _encryptionService.Encrypt(randomNumber);
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                    return "ErrorInUpdateUserCode";
                var message = $"Code To Reset Password {randomNumber}";
                var sendEmailResult = await _emailService.SendEmailAsync(email, "Reset Password", message);
                if (sendEmailResult == "Success")
                {
                    await trans.CommitAsync();
                    return "Success";
                }
                else
                    return "FailedToSendEmail";
            }
            catch (Exception)
            {

                await trans.RollbackAsync();
                return "Failed";
            }
        }

        public async Task<string> ConfirmResetPassword(string code, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return "UserNotFound";
            }
            // decrypt
            var resetPasswordCode = _encryptionService.Decrypt(user.Code);
            if (resetPasswordCode == code) return "Success";
            return "Failed";
        }

        public async Task<string> ResetPassword(string email, string paswword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return "UserNotFound";
            }
            var trans = _context.Database.BeginTransaction();
            try
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, paswword);
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {

                await trans.RollbackAsync();
                return "Failed";
            }
        }


        #endregion
    }
}
