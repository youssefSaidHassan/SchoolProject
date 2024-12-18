﻿using AutoMapper;

namespace SchoolProject.Core.Mapping.StudentMapping
{
    public partial class StudentProfile : Profile
    {
        public StudentProfile()
        {
            GetStudentMapping();
            AddStudentCommandMapping();
            EditStudentCommandMapping();
        }
    }
}
