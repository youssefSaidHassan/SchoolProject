using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.DTOs;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementation
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        #endregion

        #region Constructor
        public AuthorizationService(RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        #endregion

        #region Handel Functions
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



        public async Task<bool> IsRoleExistByName(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return false;
            }
            return true;
        }

        public async Task<string> EditRoleAsync(EditRoleRequest request)
        {
            var role = await _roleManager.FindByIdAsync(request.Id);
            if (role == null)
            {
                return "NotFound";
            }
            role.Name = request.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return "Success";
            }
            var errors = string.Join("-", result.Errors);
            return errors;
        }

        public async Task<bool> IsRoleExistById(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return false;
            }
            return true;
        }
        public async Task<string> DeleteRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var users = await _userManager.GetUsersInRoleAsync(role.Name);

            if (users.Any()) return "Used";

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded) return "Success";

            var errors = string.Join("-", result.Errors);
            return errors;
        }

        public async Task<List<IdentityRole>> GetRoleListAsync()
        {
            return await _roleManager.Roles.ToListAsync();

        }

        public async Task<IdentityRole> GetRoleByIdAsync(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }


        #endregion
    }
}
