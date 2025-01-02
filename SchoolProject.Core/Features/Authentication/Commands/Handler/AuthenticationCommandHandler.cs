using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Data.Responses;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authentication.Commands.Handler
{
    public class AuthenticationCommandHandler : ResponseHandler,
    IRequestHandler<SingInCommand, Response<JwtAuthResponse>>,
    IRequestHandler<RefreshTokenCommand, Response<JwtAuthResponse>>,
    IRequestHandler<SendResetPasswordCommand, Response<string>>,
    IRequestHandler<ResetPasswordCommand, Response<string>>
    {
        #region Fileds
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Constructor
        public AuthenticationCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            this._stringLocalizer = stringLocalizer;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._authenticationService = authenticationService;
        }

        #endregion

        #region Handel Functions
        public async Task<Response<JwtAuthResponse>> Handle(SingInCommand request, CancellationToken cancellationToken)
        {
            // Check user
            var user = await _userManager.FindByNameAsync(request.UserName);
            //return not found 
            if (user == null)
            {
                return BadRequest<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.SignInFailed]);
            }
            //sign in
            //var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (!result.Succeeded)
            {
                return BadRequest<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.SignInFailed]);
            }
            //check confirm email
            if (!user.EmailConfirmed)
                return BadRequest<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.EmailNotConfirmed]);
            //generate token
            var token = await _authenticationService.GetJWTToken(user);
            // return token
            return Success(token);
        }

        public async Task<Response<JwtAuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var token = _authenticationService.ReadJwtToken(request.AccessToken);
            var validToken = await _authenticationService.ValidateDetails(token, request.AccessToken, request.RefreshToken);
            switch (validToken)
            {

                case "Valid":
                    break;
                case "Algorithm Is Wrong":
                    return Unauthorized<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.AlgorithmIsWrong]);
                case "Token Is Not Expired":
                    return Unauthorized<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.TokenIsNotExpired]);
                case "Refresh Token Is Not Found":
                    return Unauthorized<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.RefreshTokenIsNotFound]);
                case "Refresh Token Is Not Expired":
                    return Unauthorized<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.RefreshTokenIsNotExpired]); ;
            }

            var userId = token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserId)).Value;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound<JwtAuthResponse>();
            }

            var result = await _authenticationService.GetRefreshToken(user, request.AccessToken, request.RefreshToken);

            return Success(result);
        }

        public async Task<Response<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.SendResetPasswordCode(request.Email);
            switch (result)
            {
                case "UserNotFound":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
                case "Success":
                    return Success("");
                case "ErrorInUpdateUserCode":
                case "Failed":
                case "FailedToSendEmail":
                default:
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.TryAgain]);
            }
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ResetPassword(request.Email, request.Password);
            switch (result)
            {
                case "UserNotFound":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
                case "Success":
                    return Success("");
                case "Failed":
                default:
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.TryAgain]);
            }
        }
        #endregion
    }
}
