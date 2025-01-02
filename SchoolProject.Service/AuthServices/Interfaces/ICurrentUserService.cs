using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.AuthServices.Interfaces
{
    public interface ICurrentUserService
    {
        public Task<User> GetUserAsync();
        public Task<List<string>> GetUserRolesAsync();
        public string GetUserId();
    }
}
