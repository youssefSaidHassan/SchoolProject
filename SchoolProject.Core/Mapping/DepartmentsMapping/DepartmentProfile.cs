using AutoMapper;

namespace SchoolProject.Core.Mapping.DepartmentsMapping
{
    public partial class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            GetDepartmentByIdMapping();
            GetDepartmentStudentCountMapping();
        }
    }
}
