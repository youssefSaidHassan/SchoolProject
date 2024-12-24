using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResponse> GetJWTToken(User user);

        public Task<JwtAuthResponse> GetRefreshToken(User user, string accessToken, string refreshToken);
        public JwtSecurityToken ReadJwtToken(string accessToken);
        public Task<string> ValidateDetails(JwtSecurityToken token, string accessToken, string refreshToken);

        public Task<string> ValidateToken(string accessToken);
    }
}
