using SchoolProject.Data.DTOs;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<string> DeleteRoleAsync(string roleId);
        public Task<string> EditRoleAsync(EditRoleRequest request);
        public Task<bool> IsRoleExistByName(string roleName);
        public Task<bool> IsRoleExistById(string roleId);
    }
}
