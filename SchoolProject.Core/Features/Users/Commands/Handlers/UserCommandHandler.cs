using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Users.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;

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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Constructors(s)
        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService,
            IAuthenticationService authenticationService
            ) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _authenticationService = authenticationService;
        }
        #endregion

        #region Handel Functions
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //// check for email
            //var user = await _userManager.FindByEmailAsync(request.Email);
            //if (user != null)
            //{
            //    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailIsExist]);
            //}
            // mapper 
            var userMapper = _mapper.Map<User>(request);
            // register user 
            var result = await _authenticationService.RegisterUserAsync(userMapper, request.Password);
            //return response
            switch (result)
            {
                case "FailedToSendEmail":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailFailed]);
                case "FailedToAddUser":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddUser]);
                case "Failed":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.TryToRegisterAgain]);
                case "Success":
                    return Created("");
                default:
                    return BadRequest<string>(result);
            }
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
