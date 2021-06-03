﻿using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WishMe.Service.Dtos;
using WishMe.Service.Entities;

namespace WishMe.Service.Repositories
{
  public interface IGenericRepository
  {
    Task<int> CreateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
      where TEntity : EntityBase;
    Task<ListDto<TEntity>> GetManyAsync<TEntity>(FilterDto<TEntity> filter, CancellationToken cancellationToken)
      where TEntity : EntityBase;
    Task<TEntity?> GetAsync<TEntity>(int id, CancellationToken cancellationToken)
      where TEntity : EntityBase;
    Task<TEntity?> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
      where TEntity : EntityBase;
    Task<TEntity?> GetAsync<TEntity>(int id, string[] includes, CancellationToken cancellationToken)
      where TEntity : EntityBase;
    Task<TEntity?> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter, string[] includes, CancellationToken cancellationToken)
      where TEntity : EntityBase;
    Task<bool> DeleteAsync<TEntity>(int id, CancellationToken cancellationToken)
      where TEntity : EntityBase;
    Task<bool> UpdateAsync<TEntity>(int id, object updated, CancellationToken cancellationToken)
      where TEntity : EntityBase;
    Task<bool> ExistsAsync<TEntity>(int id, CancellationToken cancellationToken)
      where TEntity : EntityBase;
  }
}