﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrastructure.Abstract;
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

        //private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshToken;

        #endregion

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings,
            IRefreshTokenRepository refreshTokenRepository,
            UserManager<User> userManager)
        {
            this._jwtSettings = jwtSettings;
            this._refreshTokenRepository = refreshTokenRepository;
            this._userManager = userManager;
            //_userRefreshToken = new ConcurrentDictionary<string, RefreshToken>();
        }

        #endregion

        #region Handel Functions
        public async Task<JwtAuthResponse> GetJWTToken(User user)
        {


            var (token, accessToken) = GenerateJwtToken(user);

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
        private List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>()
          {
                new Claim(nameof(UserClaimModel.UserName), user.UserName),
                new Claim(nameof(UserClaimModel.Email), user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
                new Claim(nameof(UserClaimModel.UserId), user.Id)
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

            var (jwtSecurityToken, newToken) = GenerateJwtToken(user);
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
        private (JwtSecurityToken, string) GenerateJwtToken(User user)
        {
            var claims = GetClaims(user);
            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddSeconds(_jwtSettings.AccessTokenExpireDate),
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


        #endregion
    }
}
