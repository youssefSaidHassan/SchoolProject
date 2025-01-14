﻿using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> GetRoleById([FromRoute] string id)
        {
            var response = await _mediator.Send(new GetRoleByIdQuery(id));
            return NewResult(response);
        }

        [HttpGet(Router.AuthorizationRouting.MangeUserRoles)]
        public async Task<IActionResult> MangeUserRoles([FromRoute] string userId)
        {
            var response = await _mediator.Send(new MangeUserRolesQuery(userId));
            return NewResult(response);
        }
        [HttpPut(Router.AuthorizationRouting.UpdateUserRoles)]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet(Router.AuthorizationRouting.MangeUserClaims)]
        public async Task<IActionResult> MangeUserClaims(string userId)
        {
            var response = await _mediator.Send(new MangeUserClaimsQuery(userId));
            return NewResult(response);
        }
        [HttpPut(Router.AuthorizationRouting.UpdateUserClaims)]
        public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
    }
}
