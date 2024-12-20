using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Users.Queries.Models;
using SchoolProject.Core.Features.Users.Queries.Responses;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.Users.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler,
        IRequestHandler<GetUserPaginatedQuery, PaginatedResult<UserResponse>>,
        IRequestHandler<GetUserByIdQuery, Response<UserResponse>>
    {

        #region Fileds
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructor(s)
        public UserQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            UserManager<User> userManager) : base(stringLocalizer)
        {
            this._stringLocalizer = stringLocalizer;
            this._mapper = mapper;
            this._userManager = userManager;
        }
        #endregion

        #region Handel Functions
        public async Task<PaginatedResult<UserResponse>> Handle(GetUserPaginatedQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            var paginatedUsers = await _mapper.ProjectTo<UserResponse>(users)
                 .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return paginatedUsers;
        }

        public async Task<Response<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByIdAsync(request.UserId);
            //var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id.Equals(request.UserId));

            if (user == null) return NotFound<UserResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var userMapper = _mapper.Map<UserResponse>(user);
            return Success(userMapper);
        }
        #endregion
    }
}
