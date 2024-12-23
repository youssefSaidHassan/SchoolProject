using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Service.Abstracts;
using System.Collections.Concurrent;
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
        private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshToken;

        #endregion

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings,
            IRefreshTokenRepository refreshTokenRepository)
        {
            this._jwtSettings = jwtSettings;
            this._refreshTokenRepository = refreshTokenRepository;
            _userRefreshToken = new ConcurrentDictionary<string, RefreshToken>();
        }

        #endregion

        #region Handel Functions
        public async Task<JwtAuthResponse> GetJWTToken(User user)
        {
            var claims = GetClaims(user);


            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials:
                new SigningCredentials(
                    new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(_jwtSettings.Secret)
                    ),
                    SecurityAlgorithms.HmacSha256Signature)
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = GetRefreshToken(user.UserName);

            var userRefreshToken = new UserRefreshToken
            {
                CreationTime = DateTime.Now,
                ExpireDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserId = user.Id,
                IsUsed = false,
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
        private List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>()
          {
                new Claim(nameof(UserClaimModel.UserName), user.UserName),
                new Claim(nameof(UserClaimModel.Email), user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber)
          };
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


            _userRefreshToken.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        #endregion
    }
}
