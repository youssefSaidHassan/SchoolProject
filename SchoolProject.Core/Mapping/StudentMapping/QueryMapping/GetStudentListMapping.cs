using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.StudentMapping
{
    public partial class StudentProfile
    {
        public void GetStudentMapping()
        {
            CreateMap<Student, StudentResponse>()
                .ForMember(dst => dst.DepartmentName, opt => opt.MapFrom(src => src.Localize(src.Department.NameAr, src.Department.NameEn)))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));
        }
    }
}
