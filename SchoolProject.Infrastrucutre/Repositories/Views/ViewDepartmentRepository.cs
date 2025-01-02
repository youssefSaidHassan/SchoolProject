using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrastructure.Abstract.Views;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories.Views
{
    public class ViewDepartmentRepository : GenericRepository<ViewDepartment>, IViewRepository<ViewDepartment>
    {
        #region Fields
        private readonly DbSet<Department> departments;
        #endregion
        #region Constructor(s)
        public ViewDepartmentRepository(ApplicationDbContext context) : base(context)
        {
            this.departments = context.Set<Department>();
        }
        #endregion

        #region Handel Functions
        #endregion
    }
}
