using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authentication.Queries.Handler
{
    public class AuthenticationQueryHandler : ResponseHandler,
    IRequestHandler<AuthorizeUserQuery, Response<string>>,
    IRequestHandler<ConfirmEmailQuery, Response<string>>
    {
        #region Fileds
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Constructor
        public AuthenticationQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            this._stringLocalizer = stringLocalizer;
            ;
            this._authenticationService = authenticationService;
        }

        #endregion

        #region Handel Functions
        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToken);
            if (result == "NotExpired") { return Success(result); }
            return Unauthorized<string>(_stringLocalizer[SharedResourcesKeys.TokenIsExpired]);
        }

        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var confirm = await _authenticationService.ConfirmEmail(request.userId, request.code);
            if (confirm == "Error")
            {
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.ErrorInConfirmEmail]);
            }
            return Success<string>(_stringLocalizer[SharedResourcesKeys.SuccessInConfirmEmail]);
        }
        #endregion
    }
}
