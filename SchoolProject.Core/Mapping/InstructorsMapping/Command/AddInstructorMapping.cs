using AutoMapper;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.InstructorsMapping
{
    public partial class InstructorProfile : Profile
    {
        public void AddInstructorMapping()
        {
            CreateMap<AddInstructorCommand, Instructor>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
        }
    }
}
