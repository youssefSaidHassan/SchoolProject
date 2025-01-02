using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Command.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Command.Handler
{
    public class RoleCommandHandler : ResponseHandler,
        IRequestHandler<AddRoleCommand, Response<string>>,
        IRequestHandler<EditRoleCommand, Response<string>>,
        IRequestHandler<DeleteRoleCommand, Response<string>>,
        IRequestHandler<UpdateUserRolesCommand, Response<string>>
    {

        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;

        #endregion

        #region Constructor
        public RoleCommandHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IAuthorizationService authorizationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
        }
        #endregion

        #region Handel Functions

        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {

            var result = await _authorizationService.AddRoleAsync(request.RoleName);
            if (result == "Success")
            {
                return Success<string>(result);
            }
            else
            {
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.AddFailed]);
            }
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.EditRoleAsync(request);
            if (result == "NotFound")
                return NotFound<string>();
            else if (result == "Success")
                return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
            else
                return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.DeleteRoleAsync(request.Id);
            if (result == "Used")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.RoleIsUsed]);
            else if (result == "Success")
                return Success<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);
            else
                return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserRoles(request);
            switch (result)
            {
                case "UserIsNull":
                    return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
                case "FailedToRemoveOldRoles":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UpdateFailed]);
                case "FailedToAddNewRoles":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UpdateFailed]);
                case "FailedToUpdateUserRoles":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UpdateFailed]);
            }
            return Success(result);
        }
        #endregion
    }
}
