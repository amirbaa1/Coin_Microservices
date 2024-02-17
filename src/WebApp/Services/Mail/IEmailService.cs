using WebApp.Model.Mail;

namespace WebApp.Services.Mail;

public interface IEmailService
{
    Task<bool> SendEmail(Email email);
}