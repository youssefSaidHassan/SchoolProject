using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Emails.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.Emails.Commands.Validators
{
    public class SendEmailValidator : AbstractValidator<SendEmailCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        #endregion
        #region Constructor
        public SendEmailValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            AppleyValidationRules();
        }
        #endregion

        #region Handel Functions
        private void AppleyValidationRules()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.Message)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

        }
        #endregion
    }
}
