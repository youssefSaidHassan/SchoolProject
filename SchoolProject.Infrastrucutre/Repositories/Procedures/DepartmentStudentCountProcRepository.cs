using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Procedures;
using SchoolProject.Infrastructure.Abstract.Procedures;
using SchoolProject.Infrastructure.Data;
using StoredProcedureEFCore;

namespace SchoolProject.Infrastructure.Repositories.Procedures
{
    public class DepartmentStudentCountProcRepository : IDepartmentStudentCountProcRepository
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        #endregion
        #region Constructor(s)
        public DepartmentStudentCountProcRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Handel Functions
        public async Task<IReadOnlyList<DepartmentStudentCountProc>> GetDepartmentStudentCountProcs(DepartmentStudentCountProcParameters parameters)
        {
            var rows = new List<DepartmentStudentCountProc>();
            await _context.LoadStoredProc(nameof(DepartmentStudentCountProc))
                .AddParam(nameof(DepartmentStudentCountProcParameters.Id), parameters.Id)
                .ExecAsync(async r => rows = await r.ToListAsync<DepartmentStudentCountProc>());

            return rows;
        }
        #endregion
    }
}
