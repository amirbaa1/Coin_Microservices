using Identity.API.Model.Mail;

namespace Identity.API.Services.Mail;

public interface IEmailService
{
    Task<bool> SendEmail(Email email);
}