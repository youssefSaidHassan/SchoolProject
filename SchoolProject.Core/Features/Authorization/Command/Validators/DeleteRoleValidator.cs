using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Command.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Command.Validators
{
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
    {
        #region Fields 
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constructor(s)
        public DeleteRoleValidator(
            IStringLocalizer<SharedResources> stringLocalizer,
            IAuthorizationService authorizationService)
        {
            this._stringLocalizer = stringLocalizer;
            this._authorizationService = authorizationService;
            AppleyValidationRules();
            AppleyCustomValidationRules();
        }

        #endregion

        #region Handel Functions 
        private void AppleyValidationRules()
        {

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

        }
        private void AppleyCustomValidationRules()
        {
            RuleFor(x => x.Id)
                .MustAsync(async (Key, CancellationToken) => await _authorizationService.IsRoleExistById(Key))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.NotExist]);
        }
        #endregion

    }
}
