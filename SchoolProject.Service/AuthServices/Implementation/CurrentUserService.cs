using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.AuthServices.Interfaces;

namespace SchoolProject.Service.AuthServices.Implementation
{
    public class CurrentUserService : ICurrentUserService
    {
        #region Fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        #endregion
        #region Constructor
        public CurrentUserService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        #endregion

        #region Functions

        public string GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(c => c.Type == nameof(UserClaimModel.UserId)).Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException();
            }
            return userId; ;
        }
        public async Task<User> GetUserAsync()
        {
            var userId = GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new UnauthorizedAccessException();
            return user;
        }

        public async Task<List<string>> GetUserRolesAsync()
        {
            var user = await GetUserAsync();
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
        #endregion
    }
}
