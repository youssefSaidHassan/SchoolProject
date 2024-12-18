using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        #region Fields
        private readonly DbSet<Department> departments;
        #endregion
        #region Constructor(s)
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
            this.departments = context.Set<Department>();
        }
        #endregion

        #region Handel Functions
        #endregion
    }
}
