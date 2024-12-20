using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Users.Commands.Models;
using SchoolProject.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Users.Commands.Validators
{
    public class EditUserValidator : AbstractValidator<EditUserCommand>
    {
        #region Fields

        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        #endregion

        #region Constructor
        public EditUserValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            this._stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
            ApplyCustomValidationRules();
        }
        #endregion

        #region Handel Functions

        public void ApplyValidationsRules()
        {
            RuleFor(u => u.FullName)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
                .MaximumLength(50).WithMessage(_stringLocalizer[SharedResourcesKeys.MaximumLength50]);
            RuleFor(u => u.UserName)
                   .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                   .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
                .EmailAddress().WithMessage(_stringLocalizer[SharedResourcesKeys.EmailAddress]);

          

        }

        public void ApplyCustomValidationRules()
        {

        }

        #endregion
    }
}
