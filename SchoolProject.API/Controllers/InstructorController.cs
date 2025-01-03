using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Bases;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Core.Features.Instructors.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    public class InstructorController : AppControllerBase
    {
        [HttpGet(Router.InstructorRouting.GetSalarySummation)]
        public async Task<IActionResult> GetSalarySummation()
        {
            return NewResult(await _mediator.Send(new GetSummationSalaryOfInstructorQuery()));
        }

        [HttpPost(Router.InstructorRouting.CreateInstructor)]
        public async Task<IActionResult> Create([FromForm] AddInstructorCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }

    }
}
