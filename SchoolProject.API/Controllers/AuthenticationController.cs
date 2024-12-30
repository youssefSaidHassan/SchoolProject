using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{

    public class AuthenticationController : AppControllerBase
    {
        [HttpPost(Router.AuthenticationRouting.SignIn)]
        public async Task<IActionResult> SignIn([FromForm] SingInCommand command)
        {
            var response = await _mediator.Send(command);

            return NewResult(response);
        }

        [HttpPost(Router.AuthenticationRouting.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand command)
        {
            var response = await _mediator.Send(command);

            return NewResult(response);
        }
        [HttpGet(Router.AuthenticationRouting.ValidateToken)]
        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery query)
        {
            var response = await _mediator.Send(query);

            return NewResult(response);
        }
        [
        HttpGet(Router.AuthenticationRouting.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
        {
            var response = await _mediator.Send(query);

            return NewResult(response);
        }
    }
}