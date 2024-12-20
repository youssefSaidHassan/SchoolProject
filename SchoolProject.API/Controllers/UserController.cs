using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Bases;
using SchoolProject.Core.Features.Users.Commands.Models;
using SchoolProject.Core.Features.Users.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    public class UserController : AppControllerBase
    {
        [HttpPost(Router.UserRouting.Create)]
        public async Task<IActionResult> AddUser([FromBody] AddUserCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet(Router.UserRouting.Paginated)]
        public async Task<IActionResult> GetAll([FromQuery] GetUserPaginatedQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        [HttpGet(Router.UserRouting.GetById)]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery(id));
            return NewResult(response);
        }
        [HttpPut(Router.UserRouting.Edit)]
        public async Task<IActionResult> EditUser([FromBody] EditUserCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
    }
}
