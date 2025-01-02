using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Command.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Command.Validators
{
    public class AddRoleValidators : AbstractValidator<AddRoleCommand>
    {
        #region Fields 
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constructor(s)
        public AddRoleValidators(
            IStringLocalizer<SharedResources> stringLocalizer,
            IAuthorizationService authorizationService)
        {
            this._stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            AppleyValidationRules();
            AppleyCustomValidationRules();
        }

        #endregion

        #region Handel Functions 
        private void AppleyValidationRules()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

        }
        private void AppleyCustomValidationRules()
        {
            RuleFor(x => x.RoleName)
                .MustAsync(async (Key, CancellationToken) => !await _authorizationService.IsRoleExistByName(Key))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.Exist]);
        }
        #endregion
    }
}
