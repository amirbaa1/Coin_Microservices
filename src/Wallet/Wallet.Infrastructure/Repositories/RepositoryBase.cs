using MongoDB.Bson;
using MongoDB.Driver;
using Wallet.Application.Contracts;
using Wallet.Domain.Common;
using Wallet.Infrastructure.Data;

namespace Wallet.Infrastructure.Repositories;

public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
{
    protected readonly WalletDbContext _walletDbContext;

    public RepositoryBase(WalletDbContext walletDbContext)
    {
        _walletDbContext = walletDbContext;
    }


    public Task<IReadOnlyList<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<T> GetByIDAsync(ObjectId id)
    {
        throw new NotImplementedException();
    }

    public Task<T> AddAsync(T entity, Func<T, BsonDocument> converter)
    {
        throw new NotImplementedException();
    }
}