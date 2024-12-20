using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementation
{
    public class DepartmentServices : IDepartmentServices
    {
        #region Fields
        private readonly IDepartmentRepository _departmentRepository;
        #endregion

        #region Constructors
        public DepartmentServices(IDepartmentRepository departmentRepository)
        {
            this._departmentRepository = departmentRepository;
        }
        #endregion

        #region Handel Functions
        public async Task<Department> GetDepartmentById(int id)
        {
            var department = await _departmentRepository.GetTableNoTracking().Where(d => d.Id == id)
                .Include(d => d.Instructors)
                .Include(d => d.DepartmentSubjects).ThenInclude(d => d.Subject)
                .Include(d => d.Instructor)
                .FirstOrDefaultAsync();

            return department;

        }

        public async Task<bool> IsDepartmentIdExist(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);

            return department != null;
        }
        #endregion
    }
}
