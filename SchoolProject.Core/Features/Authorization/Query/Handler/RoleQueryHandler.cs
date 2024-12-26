using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Query.Models;
using SchoolProject.Core.Features.Authorization.Query.Responses;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Query.Handler
{
    internal class RoleQueryHandler : ResponseHandler,
        IRequestHandler<GetRoleListQuery, Response<List<GetRoleResponse>>>,
        IRequestHandler<GetRoleByIdQuery, Response<GetRoleResponse>>
    {
        #region Fields
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;

        #endregion
        #region Constructor

        public RoleQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IAuthorizationService authorizationService,
            IMapper mapper) : base(stringLocalizer)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        #endregion


        #region Handel Functions

        async Task<Response<List<GetRoleResponse>>> IRequestHandler<GetRoleListQuery, Response<List<GetRoleResponse>>>.Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetRoleListAsync();
            var result = _mapper.Map<List<GetRoleResponse>>(roles);
            return Success(result);
        }
        public async Task<Response<GetRoleResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRoleByIdAsync(request.roleId);
            if (role == null) return NotFound<GetRoleResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var result = _mapper.Map<GetRoleResponse>(role);
            return Success(result);
        }

        #endregion
    }
}
