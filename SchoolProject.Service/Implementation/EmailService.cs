using MailKit.Net.Smtp;
using MimeKit;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementation
{
    public class EmailService : IEmailService
    {
        #region Fields
        private readonly EmailSettings _emailSettings;
        #endregion

        #region Constructor
        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }
        #endregion

        #region Handel Functions
        public async Task<string> SendEmailAsync(string email, string subject, string Message)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port);
                    client.Authenticate(_emailSettings.Email, _emailSettings.Password);
                    var bodyBuilder = new BodyBuilder
                    {
                        HtmlBody = $"{Message}",
                        TextBody = "welcome"
                    };
                    var message = new MimeMessage
                    {
                        Body = bodyBuilder.ToMessageBody()
                    };
                    message.From.Add(new MailboxAddress("Test Team", "youssef.rsys@gmail.com"));
                    message.To.Add(new MailboxAddress("testing", email));
                    message.Subject = subject;
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed";
            }
        }


        #endregion
    }
}
