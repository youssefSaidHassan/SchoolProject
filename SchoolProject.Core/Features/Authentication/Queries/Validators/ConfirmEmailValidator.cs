using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.Authentication.Queries.Validators
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailQuery>
    {

        #region Fields 
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructor(s)
        public ConfirmEmailValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            this._stringLocalizer = stringLocalizer;

            AppleyValidationRules();
            AppleyCustomValidationRules();
        }

        #endregion

        #region Handel Functions 
        private void AppleyValidationRules()
        {
            RuleFor(x => x.userId)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.code)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
        }

        private void AppleyCustomValidationRules()
        {

        }
        #endregion
    }
}

