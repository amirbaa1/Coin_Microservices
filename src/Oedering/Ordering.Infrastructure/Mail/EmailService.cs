using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrasturcture;
using Ordering.Application.Model;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace Ordering.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public EmailSetting _EmailSettings { get; }

        private readonly ILogger<EmailService> _logger;
        public EmailService(IOptions<EmailSetting> options, ILogger<EmailService> logger)
        {
            _EmailSettings = options.Value;
            _logger = logger;
        }

        public async Task<bool> sendEmail(Email email)
        {
            try
            {
                var message = new MailMessage(email.From, email.To, email.Subject, email.Body);
                // _logger.LogInformation($"Email message : {message.ToString()}");
                using (var emailClient = new SmtpClient(_EmailSettings.HOST,_EmailSettings.PORT))
                {
                    emailClient.Credentials = new NetworkCredential(_EmailSettings.User, _EmailSettings.Password);
                    await emailClient.SendMailAsync(message);
                    // _logger.LogInformation($"sand Email : {email.To}");
                };
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sanding email : {ex.Message}");
                return false;
            }
            // try
            // {
            //     //api SendGrid
            //     var client = new SendGridClient(_EmailSettings.ApiKey);
            //     if (client != null)
            //     {
            //         //info email
            //         var sub = email.Subject;
            //         var to = new EmailAddress(email.To);
            //         var emailBody = email.Body;
            //
            //         var forme = new EmailAddress
            //         {
            //             Email = _EmailSettings.FromAddress,
            //             Name = _EmailSettings.FromName
            //         };
            //
            //         var message = MailHelper.CreateSingleEmail(forme, to, sub, emailBody, emailBody);
            //         var response = await client.SendEmailAsync(message);
            //
            //         _logger.LogInformation($"email sent! To {to.Email}");
            //
            //         if (response.IsSuccessStatusCode)
            //         {
            //             return true;
            //         }
            //
            //         _logger.LogError("Email sending failed");
            //         return false;
            //     }
            //     else
            //     {
            //         _logger.LogError($"Code Erorr");
            //
            //     }
            //     _logger.LogError("Email sending failed");
            //     return false;
            // }
            // catch (Exception ex)
            // {
            //     _logger.LogError($"Code Erorr : {ex.Message}");
            //     throw ex;
            //
            // }

        }
    }
}
