using System.Data.Common;

namespace SchoolProject.Infrastructure.Abstract.Functions
{
    public interface IInstructorFunctionsRepository
    {
        public decimal GetSalarySummationOfInstructor(string query, DbCommand cmd);
    }
}
