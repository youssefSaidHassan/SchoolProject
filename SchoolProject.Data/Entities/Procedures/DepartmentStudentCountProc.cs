using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities.Procedures
{
    [Keyless]
    public class DepartmentStudentCountProc : LocalizableEntity
    {
        public int DepartmentId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public int StudentCount { get; set; }

    }
    public class DepartmentStudentCountProcParameters
    {
        public int Id { get; set; } = 0;
    }
}
