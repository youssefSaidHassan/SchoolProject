using SchoolProject.Infrastructure.Abstract.Functions;
using System.Data.Common;

namespace SchoolProject.Infrastructure.Repositories.Functions
{
    public class InstructorFunctionsRepository : IInstructorFunctionsRepository
    {
        #region Fields
        #endregion

        #region Constructor

        #endregion


        #region Handel Functions

        public decimal GetSalarySummationOfInstructor(string query, DbCommand cmd)
        {
            decimal response = 0;
            cmd.CommandText = query;
            var value = cmd.ExecuteScalar().ToString();
            if (decimal.TryParse(value, out decimal d))
            {
                response = d;
            }
            return response;
        }
        #endregion
    }
}
