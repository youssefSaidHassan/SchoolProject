using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Emails.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Emails.Commands.Handler
{
    public class EmailCommandHandler : ResponseHandler,
        IRequestHandler<SendEmailCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IEmailService _emailService;
        #endregion

        #region Constructor
        public EmailCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                IEmailService emailService
            ) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _emailService = emailService;
        }

        #endregion

        #region Handel Functions

        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _emailService.SendEmailAsync(request.Email, request.Message);
            if (response == "Success")
                return Success<string>("");
            else
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailFailed]);
        }

        #endregion
    }
}
