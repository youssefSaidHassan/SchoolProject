﻿using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Queries.Response;

namespace SchoolProject.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentStudentCountQuery : IRequest<Response<List<GetDepartmentStudentCountResponse>>>
    {
    }
}
