using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstracts
{
    public interface IDepartmentServices
    {
        public Task<Department> GetDepartmentById(int id);
    }
}
