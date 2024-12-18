using FluentValidation;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validatiors
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        #endregion
        #region Constructors
        public EditStudentValidator(IStudentService studentService)
        {
            this._studentService = studentService;
            ApplyeValidationRules();
            ApplyeCustomValidationRules();
        }
        #endregion

        #region Handel Method
        private void ApplyeValidationRules()
        {
            RuleFor(s => s.NameEn)
                .NotEmpty().WithMessage("Name must not be empty")
                .NotNull().WithMessage("Name must not be null")
                .MaximumLength(10).WithMessage("the name length must be less than 10");
            RuleFor(s => s.NameAr)
                .NotEmpty().WithMessage("Name must not be empty")
                .NotNull().WithMessage("Name must not be null")
                .MaximumLength(10).WithMessage("the name length must be less than 10");

            RuleFor(s => s.Address)
                .NotEmpty().WithMessage("{PropertyName} must not be empty")
                .NotNull().WithMessage("{PropertyName} must be not null")
                .MaximumLength(10).WithMessage("{PropertyValue} length is 10");

            RuleFor(s => s.Phone)
                .Matches("^01[0125][0-9]{8}$").WithMessage("Invalid Phone");
        }

        private void ApplyeCustomValidationRules()
        {
            RuleFor(s => s.NameEn)
                .MustAsync(async (model, Key, CancellationToken) => !await _studentService.IsNameExistExcludeSelf(Key, model.Id)
                ).WithMessage("Name Is Exist");
            RuleFor(s => s.NameAr)
                .MustAsync(async (model, Key, CancellationToken) => !await _studentService.IsNameExistExcludeSelf(Key, model.Id)
                ).WithMessage("Name Is Exist");

        }
        #endregion


    }
}
