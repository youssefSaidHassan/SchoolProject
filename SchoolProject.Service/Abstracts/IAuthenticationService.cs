using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Responses;
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
        public Task<string> RegisterUserAsync(User user, string password);
        public Task<string> ConfirmEmail(string useIdr, string code);
        public Task<string> SendResetPasswordCode(string email);
        public Task<string> ConfirmResetPassword(string code, string email);
        public Task<string> ResetPassword(string email, string paswword);
    }
}
