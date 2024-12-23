﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authentication.Commands.Handler
{
    public class AuthenticationCommandHandler : ResponseHandler,
    IRequestHandler<SingInCommand, Response<JwtAuthResponse>>
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

            if (result.Succeeded)
            {

                var token = await _authenticationService.GetJWTToken(user);
                // return token
                return Success(token);
            }
            return BadRequest<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.SignInFailed]);
            //generate token
        }
        #endregion
    }
}