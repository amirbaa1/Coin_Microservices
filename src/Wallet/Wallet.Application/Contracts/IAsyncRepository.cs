using System.Linq.Expressions;
using MongoDB.Bson;
using Wallet.Domain.Common;

namespace Wallet.Application.Contracts;

public interface IAsyncRepository<T> where T : EntityBase
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> GetByIDAsync(ObjectId id);
    Task<T> AddAsync(T entity, Func<T, BsonDocument> converter);
}