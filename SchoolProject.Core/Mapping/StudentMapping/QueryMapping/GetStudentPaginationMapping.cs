using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.StudentMapping
{
    public partial class StudentProfile
    {
        public void GetStudentPaginationMapping()
        {
            CreateMap<Student, GetStudentPaginatedListResponse>()
              .ForMember(dst => dst.DepartmentName, opt => opt.MapFrom(src => src.Localize(src.Department.NameAr, src.Department.NameEn)))
              .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
        }
    }
}
