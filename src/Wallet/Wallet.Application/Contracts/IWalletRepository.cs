namespace Wallet.Application.Contracts;

public interface IWalletRepository : IAsyncRepository<Domain.Entities.Wallet>
{
    Task<IEnumerable<Domain.Entities.Wallet>> GetWalletByUsername(string username);
}