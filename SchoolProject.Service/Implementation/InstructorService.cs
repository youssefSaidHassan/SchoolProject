using Microsoft.EntityFrameworkCore;
using SchoolProject.Infrastructure.Abstract.Functions;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Service.Abstracts;
using System.Data;

namespace SchoolProject.Service.Implementation
{
    public class InstructorService : IInstructorService
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        private readonly IInstructorFunctionsRepository _instructorFunctionsRepository;
        #endregion

        #region Constructor
        public InstructorService(ApplicationDbContext context, IInstructorFunctionsRepository instructorFunctionsRepository)
        {
            _context = context;
            _instructorFunctionsRepository = instructorFunctionsRepository;
        }
        #endregion


        #region Handel Functions
        public Task<decimal> GetSalarySummationOfInstructor()
        {
            decimal result = 0;
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }
                result = _instructorFunctionsRepository.GetSalarySummationOfInstructor("select dbo.GetSalarySummation()", cmd);
            }
            return Task.FromResult(result);
        }

        #endregion
    }
}
