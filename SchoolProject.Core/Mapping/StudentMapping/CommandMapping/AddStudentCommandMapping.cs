﻿using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.StudentMapping
{
    public partial class StudentProfile
    {
        public void AddStudentCommandMapping()
        {
            CreateMap<AddStudentCommand, Student>()
                .ForMember(dest => dest.DepartmentID, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.NameAr))
                .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn));


        }
    }
}
