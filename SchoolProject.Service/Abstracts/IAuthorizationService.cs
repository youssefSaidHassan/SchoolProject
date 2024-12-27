using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Responses;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<string> DeleteRoleAsync(string roleId);
        public Task<string> EditRoleAsync(EditRoleRequest request);
        public Task<bool> IsRoleExistByName(string roleName);
        public Task<bool> IsRoleExistById(string roleId);
        public Task<List<IdentityRole>> GetRoleListAsync();
        public Task<IdentityRole> GetRoleByIdAsync(string id);
        public Task<MangeUserRolesResponse> GetMangeUserRolesData(User user);
        public Task<string> UpdateUserRoles(UpdateUserRolesRequest request);
        public Task<string> UpdateUserClaims(UpdateUserClaimsRequest request);
        public Task<MangeUserClaimsResponse> GetMangeUserClaimData(User user);
    }
}
