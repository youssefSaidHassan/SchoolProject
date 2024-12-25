using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Command.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Command.Handler
{
    public class RoleCommandHandler : ResponseHandler,
        IRequestHandler<AddRoleCommand, Response<string>>
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
        #endregion
    }
}
