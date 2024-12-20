using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Users.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.Users.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
         IRequestHandler<AddUserCommand, Response<string>>,
         IRequestHandler<DeleteUserCommand, Response<string>>,
         IRequestHandler<ChangeUserPasswordCommand, Response<string>>,
         IRequestHandler<EditUserCommand, Response<string>>
    {
        #region fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        #endregion

        #region Constructors(s)
        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            UserManager<User> userManager)
            : base(stringLocalizer)
        {
            this._stringLocalizer = stringLocalizer;
            this._mapper = mapper;
            this._userManager = userManager;
        }
        #endregion

        #region Handel Functions
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            // check for email
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailIsExist]);
            }
            // mapper 
            var userMapper = _mapper.Map<User>(request);
            // register user 
            var result = await _userManager.CreateAsync(userMapper, request.Password);
            //return response
            if (result.Succeeded)
            {
                return Created("");
            }
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddUser]);
        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            }
            var userMapper = _mapper.Map(request, user);

            var result = await _userManager.UpdateAsync(userMapper);
            if (result.Succeeded)
            {
                return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
            }
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UpdateFailed]);

        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded) return Deleted<string>();
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.DeleteFailed]);
        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            // Get User 
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (result.Succeeded) return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);

            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UpdateFailed]);
        }
        #endregion
    }
}
