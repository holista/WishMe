using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using WishMe.Service.Database;
using WishMe.Service.Dtos;

namespace WishMe.Service.Repositories
{
  public interface IGenericRepository
  {
    Task<ObjectId> CreateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
      where TEntity : DbDoc;
    Task<ListDto<TEntity>> GetManyAsync<TEntity>(FilterDto<TEntity> filter, CancellationToken cancellationToken)
      where TEntity : DbDoc;
    Task<TEntity?> GetAsync<TEntity>(ObjectId id, CancellationToken cancellationToken)
      where TEntity : DbDoc;
    Task<TEntity?> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
      where TEntity : DbDoc;
    Task<bool> DeleteAsync<TEntity>(ObjectId id, CancellationToken cancellationToken)
         where TEntity : DbDoc;
    Task<bool> UpdateAsync<TDoc>(ObjectId id, UpdateDefinition<TDoc> update, CancellationToken cancellationToken)
      where TDoc : DbDoc;
    Task<bool> UpdateAsync<TEntity>(ObjectId id, TEntity updated, CancellationToken cancellationToken)
      where TEntity : DbDoc;
    Task<bool> ExistsAsync<TEntity>(ObjectId id, CancellationToken cancellationToken)
      where TEntity : DbDoc;
    Task<bool> ExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
      where TEntity : DbDoc;
  }
}
