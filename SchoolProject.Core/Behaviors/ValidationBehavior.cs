using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        #region Fileds
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructor(s)
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, IStringLocalizer<SharedResources> stringLocalizer)
        {
            this._validators = validators;
            this._stringLocalizer = stringLocalizer;
        }
        #endregion
        #region Handel Methods
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResult = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResult.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    var message = failures.Select(f => _stringLocalizer[f.PropertyName] + " : " + _stringLocalizer[f.ErrorMessage]).FirstOrDefault();
                    throw new ValidationException(message);
                }
            }
            return await next();
        }
        #endregion
    }
}
