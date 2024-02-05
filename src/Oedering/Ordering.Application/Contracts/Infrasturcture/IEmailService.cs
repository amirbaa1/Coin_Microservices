using Ordering.Application.Model;


namespace Ordering.Application.Contracts.Infrasturcture
{
    public interface IEmailService
    {
        Task<bool> sendEmail(Email email);
    }
}
