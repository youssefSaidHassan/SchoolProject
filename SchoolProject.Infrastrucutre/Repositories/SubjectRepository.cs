using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        #region Fields
        private readonly DbSet<Subject> subjects;
        #endregion
        #region Constructor(s)
        public SubjectRepository(ApplicationDbContext context) : base(context)
        {
            this.subjects = context.Set<Subject>();
        }
        #endregion

        #region Handel Functions
        #endregion
    }
}
