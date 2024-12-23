using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResponse> GetJWTToken(User user);
        public Task<JwtAuthResponse> GetRefreshToken(string accessToken, string refreshToken);
        public Task<string> ValidateToken(string accessToken);
    }
}
