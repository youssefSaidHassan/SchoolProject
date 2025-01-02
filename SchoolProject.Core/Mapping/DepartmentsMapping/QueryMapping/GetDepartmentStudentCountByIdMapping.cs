using AutoMapper;
using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Core.Features.Departments.Queries.Response;
using SchoolProject.Data.Entities.Procedures;

namespace SchoolProject.Core.Mapping.DepartmentsMapping
{
    public partial class DepartmentProfile : Profile
    {
        public void GetDepartmentStudentCountByIdMapping()
        {
            CreateMap<GetDepartmentStudentCountByIdQuery, DepartmentStudentCountProcParameters>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DID));

            CreateMap<DepartmentStudentCountProc, GetDepartmentStudentCountByIdResponse>()
                   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))
                   .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.StudentCount));
        }
    }
}
