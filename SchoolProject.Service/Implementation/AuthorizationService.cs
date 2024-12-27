using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.DTOs;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementation
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructor
        public AuthorizationService(RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
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

        public async Task<MangeUserRolesResponse> GetMangeUserRolesData(User user)
        {
            var response = new MangeUserRolesResponse();
            // user roles
            var userRoles = await _userManager.GetRolesAsync(user);
            response.UserId = user.Id;
            // all roles
            // if roles contain user roles true or false
            var roles = await _roleManager.Roles.ToListAsync();
            foreach (var item in roles)
            {
                var role = new UserRoles();
                role.HasRole = userRoles.Contains(item.Name);
                role.RoleName = item.Name;
                role.RoleId = item.Id;
                response.Roles.Add(role);
            }
            return response;
        }

        public async Task<string> UpdateUserRoles(UpdateUserRolesRequest request)
        {
            var transact = await _context.Database.BeginTransactionAsync();
            try
            {


                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    return "UserIsNull";
                }

                var userRoles = await _userManager.GetRolesAsync(user);
                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);

                if (!removeResult.Succeeded)
                {
                    return "FailedToRemoveOldRoles";
                }

                var selectedRoles = request.Roles.Where(r => r.HasRole == true).Select(r => r.RoleName);
                var addResult = await _userManager.AddToRolesAsync(user, selectedRoles);

                if (!addResult.Succeeded)
                {
                    return "FailedToAddNewRoles";

                }
                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateUserRoles";
            }
        }


        #endregion
    }
}
