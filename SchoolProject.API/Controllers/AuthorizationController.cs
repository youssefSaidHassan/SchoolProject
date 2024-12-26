using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Bases;
using SchoolProject.Core.Features.Authorization.Command.Models;
using SchoolProject.Core.Features.Authorization.Query.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AuthorizationController : AppControllerBase
    {
        [HttpPost(Router.AuthorizationRouting.CreateRole)]
        public async Task<IActionResult> CreateRole([FromForm] AddRoleCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost(Router.AuthorizationRouting.EditRole)]
        public async Task<IActionResult> EditRole([FromForm] EditRoleCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete(Router.AuthorizationRouting.DeleteRole)]
        public async Task<IActionResult> EditRole([FromRoute] string id)
        {
            var response = await _mediator.Send(new DeleteRoleCommand(id));
            return NewResult(response);
        }

        [HttpGet(Router.AuthorizationRouting.GetAll)]
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await _mediator.Send(new GetRoleListQuery());
            return NewResult(response);
        }
        [HttpGet(Router.AuthorizationRouting.GetById)]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var response = await _mediator.Send(new GetRoleByIdQuery(id));
            return NewResult(response);
        }
    }
}
