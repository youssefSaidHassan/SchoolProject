using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Query.Models;
using SchoolProject.Core.Features.Authorization.Query.Responses;
using SchoolProject.Core.Resources;
using SchoolProject.Data.DTOs;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Query.Handler
{
    internal class RoleQueryHandler : ResponseHandler,
        IRequestHandler<GetRoleListQuery, Response<List<GetRoleResponse>>>,
        IRequestHandler<GetRoleByIdQuery, Response<GetRoleResponse>>,
        IRequestHandler<MangeUserRolesQuery, Response<MangeUserRolesResponse>>
    {
        #region Fields
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;

        #endregion
        #region Constructor

        public RoleQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IAuthorizationService authorizationService,
            UserManager<User> userManager,
            IMapper mapper) : base(stringLocalizer)
        {
            _authorizationService = authorizationService;
            _userManager = userManager;
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

        public async Task<Response<MangeUserRolesResponse>> Handle(MangeUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.userId);
            if (user == null) return NotFound<MangeUserRolesResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var result = await _authorizationService.GetMangeUserRolesData(user);
            return Success(result);
        }

        #endregion
    }
}
