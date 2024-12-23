using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authentication.Queries.Handler
{
    public class AuthenticationQueryHandler : ResponseHandler,
    IRequestHandler<AuthorizeUserQuery, Response<string>>
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
            return BadRequest<String>("Expired");
        }
        #endregion
    }
}
