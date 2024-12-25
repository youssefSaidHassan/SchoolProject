using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Bases;
using SchoolProject.Core.Features.Authorization.Command.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{

    public class AuthorizationController : AppControllerBase
    {
        [HttpPost(Router.AuthorizationRouting.CreateRole)]
        public async Task<IActionResult> CreateRole([FromBody] AddRoleCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
    }
}
