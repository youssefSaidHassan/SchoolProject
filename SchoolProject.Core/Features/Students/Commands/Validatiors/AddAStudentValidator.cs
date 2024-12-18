using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validatiors
{
    public class AddAStudentValidator : AbstractValidator<AddStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion
        #region Constructors
        public AddAStudentValidator(IStudentService studentService, IStringLocalizer<SharedResources> stringLocalizer)
        {
            this._studentService = studentService;
            this._stringLocalizer = stringLocalizer;
            ApplyeValidationRules();
            ApplyeCustomValidationRules();
        }
        #endregion

        #region Handel Method
        private void ApplyeValidationRules()
        {
            RuleFor(s => s.NameEn)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
                .MaximumLength(10).WithMessage(_stringLocalizer[SharedResourcesKeys.MaximumLength10]);

            RuleFor(s => s.Address)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
                .MaximumLength(10).WithMessage(_stringLocalizer[SharedResourcesKeys.MaximumLength10]);

            RuleFor(s => s.Phone)
                .Matches("^01[0125][0-9]{8}$").WithMessage(_stringLocalizer[SharedResourcesKeys.PhonePattern]);
        }

        private void ApplyeCustomValidationRules()
        {
            RuleFor(s => s.NameEn)
                .MustAsync(async (Key, CancellationToken) => !await _studentService.IsNameExist(Key)
                ).WithMessage(_stringLocalizer[SharedResourcesKeys.Exist]);
            RuleFor(s => s.NameAr)
               .MustAsync(async (Key, CancellationToken) => !await _studentService.IsNameExist(Key)
               ).WithMessage(_stringLocalizer[SharedResourcesKeys.Exist]);
        }
        #endregion

    }

}
