using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{

    [ApiController]
    [Authorize(Roles = "Admin")]
    public class StudentController : AppControllerBase
    {

        [HttpGet(Router.StudentRouting.List)]
        public async Task<IActionResult> GetStudentList()
        {
            var response = await _mediator.Send(new GetStudentListQuery());
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpGet(Router.StudentRouting.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetStudentPaginatedListQuery query)
        {

            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet(Router.StudentRouting.GetById)]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetStudentByIDQuery(id));
            return NewResult(response);
        }
        [Authorize(Policy = "CreateStudent")]
        [HttpPost(Router.StudentRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddStudentCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        [Authorize(Policy = "EditStudent")]

        [HttpPut(Router.StudentRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditStudentCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        [Authorize(Policy = "DeleteStudent")]
        [HttpDelete(Router.StudentRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteStudentCommand(id));
            return NewResult(response);
        }
    }
}
