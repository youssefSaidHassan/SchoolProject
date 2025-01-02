using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Bases;
using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    public class DepartmentController : AppControllerBase
    {
        [HttpGet(Router.DepartmentRouting.GetById)]
        public async Task<IActionResult> GetDepartmentById([FromQuery] GetDepartmentByIdQuery query)
        {
            return NewResult(await _mediator.Send(query));
        }

        [HttpGet(Router.DepartmentRouting.GetDepartmentStudentCount)]
        public async Task<IActionResult> GetDepartmentStudentCount()
        {
            return NewResult(await _mediator.Send(new GetDepartmentStudentListCountQuery()));
        }

        [HttpGet(Router.DepartmentRouting.GetDepartmentStudentCountById)]
        public async Task<IActionResult> GetDepartmentStudentCountById([FromRoute] int Id)
        {
            return NewResult(await _mediator.Send(new GetDepartmentStudentCountByIdQuery(Id)));
        }
    }
}
