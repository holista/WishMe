using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using WishMe.Service.Database;
using WishMe.Service.Dtos;

namespace WishMe.Service.Repositories
{
  public class GenericRepository: IGenericRepository
  {
    private readonly IDbContext fDbContext;

    protected IMongoCollection<TDoc> Collection<TDoc>() => fDbContext.Database.GetCollection<TDoc>();

    protected IMongoQueryable<TDoc> Query<TDoc>() => Collection<TDoc>().AsQueryable();

    public GenericRepository(IDbContext dbContext)
    {
      fDbContext = dbContext;
    }

    public async Task<ObjectId> CreateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
      where TEntity : DbDoc
    {
      await Collection<TEntity>()
          .InsertOneAsync(entity, null, cancellationToken);

      return entity.Id;
    }

    public async Task<ListDto<TEntity>> GetManyAsync<TEntity>(FilterDto<TEntity> filter, CancellationToken cancellationToken) where TEntity : DbDoc
    {
      var filtered = Query<TEntity>()
        .Where(filter.Filter);

      if (filter.SortingKeys != null)
        filtered = filtered.OrderBySortingKeys(filter.SortingKeys);

      return new ListDto<TEntity>
      {
        TotalCount = await filtered.CountAsync(cancellationToken),
        Entities = await filtered
          .Skip(filter.Offset)
          .Take(filter.Limit)
          .ToListAsync(cancellationToken)
      };
    }

    public async Task<TResult?> GetAsync<TDoc, TResult>(ObjectId id, Expression<Func<TDoc, TResult>> selection, CancellationToken cancellationToken) where TDoc : DbDoc
    {
      return await GetAsync(doc => doc.Id == id, selection, cancellationToken);
    }

    public async Task<TResult?> GetAsync<TDoc, TResult>(Expression<Func<TDoc, bool>> filter, Expression<Func<TDoc, TResult>> selection, CancellationToken cancellationToken) where TDoc : DbDoc
    {
      return await Query<TDoc>()
          .Where(filter)
          .Select(selection)
          .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<TDoc?> GetAsync<TDoc>(ObjectId id, CancellationToken cancellationToken) where TDoc : DbDoc
    {
      return await GetAsync<TDoc>(doc => doc.Id == id, cancellationToken);
    }

    public async Task<TDoc?> GetAsync<TDoc>(Expression<Func<TDoc, bool>> filter, CancellationToken cancellationToken) where TDoc : DbDoc
    {
      return await Query<TDoc>()
          .Where(filter)
          .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<List<TDoc>> GetManyAsync<TDoc>(Expression<Func<TDoc, bool>> filter, CancellationToken cancellationToken) where TDoc : DbDoc
    {
      return await Query<TDoc>()
          .Where(filter)
          .ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync<TDoc>(ObjectId id, UpdateDefinition<TDoc> update, CancellationToken cancellationToken) where TDoc : DbDoc
    {
      var result = await Collection<TDoc>()
          .UpdateOneAsync(doc => doc.Id == id, update, null, cancellationToken);

      return result.ModifiedCount == 1;
    }

    public async Task<bool> DeleteAsync<TDoc>(ObjectId id, CancellationToken cancellationToken) where TDoc : DbDoc
    {
      var result = await Collection<TDoc>()
          .DeleteOneAsync(doc => doc.Id == id, null, cancellationToken);

      return result.DeletedCount == 1;
    }

    public async Task<bool> UpdateAsync<TEntity>(ObjectId id, TEntity updated, CancellationToken cancellationToken) where TEntity : DbDoc
    {
      updated.Id = id;

      var result = await Collection<TEntity>()
        .ReplaceOneAsync(doc => doc.Id == id, updated, (ReplaceOptions?)null, cancellationToken);

      return result.ModifiedCount == 1;
    }

    public async Task<bool> ExistsAsync<TDoc>(ObjectId id, CancellationToken cancellationToken) where TDoc : DbDoc
    {
      return await Query<TDoc>()
          .CountAsync(doc => doc.Id == id, cancellationToken) == 1;
    }

    public async Task<bool> ExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken) where TEntity : DbDoc
    {
      return await Query<TEntity>()
        .Where(filter)
        .CountAsync(cancellationToken) == 1;
    }
  }
}
