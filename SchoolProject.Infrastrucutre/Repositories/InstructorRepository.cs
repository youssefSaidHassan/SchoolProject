using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class InstructorRepository : GenericRepository<Instructor>, IInstructorRepository
    {
        #region Fields
        private readonly DbSet<Instructor> instructors;
        #endregion
        #region Constructor(s)
        public InstructorRepository(ApplicationDbContext context) : base(context)
        {
            this.instructors = context.Set<Instructor>();
        }
        #endregion

        #region Handel Functions
        #endregion
    }
}

