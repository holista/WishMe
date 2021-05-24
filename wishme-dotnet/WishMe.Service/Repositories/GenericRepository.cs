using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WishMe.Service.Dtos;
using WishMe.Service.Entities;

namespace WishMe.Service.Repositories
{
  public class GenericRepository: IGenericRepository
  {
    protected readonly DataContext fContext;

    public GenericRepository(DataContext context)
    {
      fContext = context;
    }

    public async Task<int> CreateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
      where TEntity : EntityBase
    {
      await fContext.Set<TEntity>().AddAsync(entity, cancellationToken);

      await fContext.SaveChangesAsync(cancellationToken);

      return entity.Id;
    }

    public async Task<ListDto<TEntity>> GetManyAsync<TEntity>(FilterDto<TEntity> filter, CancellationToken cancellationToken)
      where TEntity : EntityBase
    {
      var filtered = fContext
        .Set<TEntity>()
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

    public async Task<TEntity?> GetAsync<TEntity>(int id, CancellationToken cancellationToken)
      where TEntity : EntityBase
    {
      return await GetAsync<TEntity>(row => row.Id == id, cancellationToken);
    }

    public async Task<TEntity?> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
      where TEntity : EntityBase
    {
      return await fContext.Set<TEntity>()
        .SingleOrDefaultAsync(filter, cancellationToken);
    }

    public async Task<TEntity?> GetAsync<TEntity>(int id, string[] includes, CancellationToken cancellationToken)
      where TEntity : EntityBase
    {
      return await GetAsync<TEntity>(row => row.Id == id, includes, cancellationToken);
    }

    public async Task<TEntity?> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter, string[] includes, CancellationToken cancellationToken)
      where TEntity : EntityBase
    {
      var collection = fContext.Set<TEntity>();

      foreach (string include in includes)
        collection.Include(include);

      return await collection
        .SingleOrDefaultAsync(filter, cancellationToken);
    }

    public async Task<bool> DeleteAsync<TEntity>(int id, CancellationToken cancellationToken)
      where TEntity : EntityBase
    {
      var entity = await GetAsync<TEntity>(id, cancellationToken);
      if (entity is null)
        return false;

      fContext.Set<TEntity>().Remove(entity);

      await fContext.SaveChangesAsync(cancellationToken);

      return true;
    }

    public async Task<bool> UpdateAsync<TEntity>(int id, object updated, CancellationToken cancellationToken)
      where TEntity : EntityBase
    {
      var entity = await GetAsync<TEntity>(id, cancellationToken);
      if (entity is null)
        return false;

      entity.UpdatedUtc = DateTime.UtcNow;

      var entry = fContext.Entry(entity);

      entry.CurrentValues.SetValues(updated);

      await fContext.SaveChangesAsync(cancellationToken);

      return true;
    }

    public async Task<bool> ExistsAsync<TEntity>(int id, CancellationToken cancellationToken)
      where TEntity : EntityBase
    {
      var entity = await GetAsync<TEntity>(id, cancellationToken);

      return entity != null;
    }
  }
}
