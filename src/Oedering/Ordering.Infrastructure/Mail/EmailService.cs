using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrasturcture;
using Ordering.Application.Model;
using System.Net;
using System.Net.Mail;


namespace Ordering.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailSetting> _options;

        private readonly ILogger<EmailService> _logger;
        public EmailService(IOptions<EmailSetting> options, ILogger<EmailService> logger)
        {
            _options = options;
            _logger = logger;
        }

        public async Task<bool> sendEmail(Email email)
        {
            try
            {
                var message = new MailMessage(email.From, email.To, email.Subject, email.Body);
                _logger.LogInformation($"Email message : {message.ToString()}");
                using (var emailClient = new SmtpClient(_options.Value.Host, _options.Value.Port))
                {
                    emailClient.Credentials = new NetworkCredential(_options.Value.UserNam, _options.Value.Password);
                    await emailClient.SendMailAsync(message);
                    _logger.LogInformation($"sand Email : {email.To}");
                };
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sanding email : {ex.Message}");
                return false;
            }
        }
    }
}
