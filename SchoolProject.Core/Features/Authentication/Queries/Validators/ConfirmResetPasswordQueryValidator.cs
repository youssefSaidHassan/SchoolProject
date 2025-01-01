using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.Authentication.Commands.Validators
{
    public class ConfirmResetPasswordQueryValidator : AbstractValidator<ConfirmResetPasswordQuery>
    {
        #region Fields 
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructor(s)
        public ConfirmResetPasswordQueryValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            this._stringLocalizer = stringLocalizer;

            AppleyValidationRules();
        }

        #endregion

        #region Handel Functions 
        private void AppleyValidationRules()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
        }


        #endregion
    }
}
