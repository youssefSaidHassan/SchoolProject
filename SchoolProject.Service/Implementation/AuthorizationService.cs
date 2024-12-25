using Microsoft.AspNetCore.Identity;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementation
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<IdentityRole> _roleManager;

        #endregion

        #region Constructor
        public AuthorizationService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        #endregion
        public async Task<string> AddRoleAsync(string roleName)
        {
            var role = new IdentityRole(roleName);
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return "Success";
            }
            return "Failed";
        }

        public async Task<bool> IsRoleExist(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return false;
            }
            return true;
        }

        #region Handel Functions

        #endregion
    }
}
