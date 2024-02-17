using System.Net;
using System.Net.Mail;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using WebApp.Model.Mail;

namespace WebApp.Services.Mail;

public class EmailService : IEmailService
{
    public EmailSettings _emailSettings { get; }
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        _logger = logger;
        _emailSettings = emailSettings.Value;
    }


    public async Task<bool> SendEmail(Email email)
    {
        try
        {
            var messageEmail = new MailMessage(email.From, email.To, email.Subject, email.Body);
            using (var emailClient = new SmtpClient(_emailSettings.HOST, _emailSettings.PORT))
            {
                emailClient.Credentials = new NetworkCredential(_emailSettings.User, _emailSettings.Password);
                await emailClient.SendMailAsync(messageEmail);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error sanding email : {ex.Message}");
            return false;
        }
    }
}