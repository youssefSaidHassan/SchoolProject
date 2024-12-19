﻿using AutoMapper;
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
         IRequestHandler<AddUserCommand, Response<string>>
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
            var userMapping = _mapper.Map<User>(request);
            // register user 
            var result = await _userManager.CreateAsync(userMapping, request.Password);
            //return response
            if (result.Succeeded)
            {
                return Created("");
            }
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FaildToAddUser]);
        }
        #endregion
    }
}