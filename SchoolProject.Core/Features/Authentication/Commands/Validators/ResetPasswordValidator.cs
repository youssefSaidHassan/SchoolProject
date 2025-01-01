using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.Authentication.Commands.Validators
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        #region Fields 
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructor(s)
        public ResetPasswordValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            this._stringLocalizer = stringLocalizer;

            AppleyValidationRules();
        }

        #endregion

        #region Handel Functions 
        private void AppleyValidationRules()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.PasswordConfirmation)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
                .Equal(p => p.Password).WithMessage(_stringLocalizer[SharedResourcesKeys.PasswordNotMatches]);
        }
        #endregion
    }
}
