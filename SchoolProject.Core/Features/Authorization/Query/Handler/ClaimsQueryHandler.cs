using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Query.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Responses;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Query.Handler
{
    public class ClaimsQueryHandler : ResponseHandler,
        IRequestHandler<MangeUserClaimsQuery, Response<MangeUserClaimsResponse>>
    {
        #region Fileds

        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<User> _userManager;


        #endregion

        #region Constructor
        public ClaimsQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IAuthorizationService authorizationService,
            UserManager<User> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        #endregion

        #region Handel Functions
        public async Task<Response<MangeUserClaimsResponse>> Handle(MangeUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) return NotFound<MangeUserClaimsResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var result = await _authorizationService.GetMangeUserClaimData(user);
            return Success(result);
        }



        #endregion
    }
}
