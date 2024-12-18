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
        private readonly IDepartmentServices _departmentServices;

        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion
        #region Constructors
        public AddAStudentValidator(IStudentService studentService, IStringLocalizer<SharedResources> stringLocalizer,
            IDepartmentServices departmentServices)
        {
            this._studentService = studentService;
            this._departmentServices = departmentServices;
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

            RuleFor(s => s.DepartmentId)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
        }

        private void ApplyeCustomValidationRules()
        {
            RuleFor(s => s.NameEn)
                .MustAsync(async (Key, CancellationToken) => !await _studentService.IsNameExist(Key)
                ).WithMessage(_stringLocalizer[SharedResourcesKeys.Exist]);
            RuleFor(s => s.NameAr)
               .MustAsync(async (Key, CancellationToken) => !await _studentService.IsNameExist(Key)
               ).WithMessage(_stringLocalizer[SharedResourcesKeys.Exist]);


            RuleFor(s => s.DepartmentId)
               .MustAsync(async (Key, CancellationToken) => await _departmentServices.IsDepartmentIdExist(Key)
               ).WithMessage(_stringLocalizer[SharedResourcesKeys.NotExist]);


        }
        #endregion

    }

}
