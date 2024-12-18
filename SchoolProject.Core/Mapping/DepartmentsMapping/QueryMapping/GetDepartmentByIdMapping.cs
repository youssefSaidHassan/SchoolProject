using SchoolProject.Core.Features.Departments.Queries.Response;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.DepartmentsMapping
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentByIdMapping()
        {
            CreateMap<Department, GetDepartmentByIdQueryResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MangerName, opt => opt.MapFrom(src => src.Localize(src.Instructor.NameAr, src.Instructor.NameEn)))
                .ForMember(dest => dest.Subjects, opt => opt.MapFrom(src => src.DepartmentSubjects))
                .ForMember(dest => dest.Instructors, opt => opt.MapFrom(src => src.Instructors))
                .ForMember(dest => dest.Students, opt => opt.Ignore());
            //.ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students));
            CreateMap<DepartmentSubject, SubjectResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubjectId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject.Localize(src.Subject.NameAr, src.Subject.NameEn)));

            //CreateMap<Student, StudentResponse>()
            //   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));

            CreateMap<Instructor, InstructorResponse>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));
        }
    }
}
