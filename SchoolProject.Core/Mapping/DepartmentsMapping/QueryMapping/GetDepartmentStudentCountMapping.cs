using AutoMapper;
using SchoolProject.Core.Features.Departments.Queries.Response;
using SchoolProject.Data.Entities.Views;

namespace SchoolProject.Core.Mapping.DepartmentsMapping
{
    public partial class DepartmentProfile : Profile
    {
        public void GetDepartmentStudentCountMapping()
        {
            CreateMap<ViewDepartment, GetDepartmentStudentCountResponse>()
                   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))
                   .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.StudentCount));
        }
    }
}
