using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WishMe.Service.Entities;

namespace WishMe.Service.Middlewares
{
  public sealed class DbTransactionMiddleware
  {
    private readonly RequestDelegate fNext;

    public DbTransactionMiddleware(RequestDelegate next)
    {
      fNext = next;
    }

    public async Task InvokeAsync(HttpContext context, DataContext dataContext)
    {
      if (context is null)
        throw new ArgumentNullException(nameof(context));
      if (dataContext is null)
        throw new ArgumentNullException(nameof(dataContext));

      var isolationLevel = context.Request.Method == HttpMethods.Get
        ? IsolationLevel.ReadCommitted
        : IsolationLevel.Serializable;

      var transaction = await dataContext.Database.BeginTransactionAsync(isolationLevel);
      try
      {
        await fNext(context);

        await dataContext.SaveChangesAsync(context.RequestAborted);

        await transaction.CommitAsync();
      }
      catch (Exception)
      {
        await transaction.RollbackAsync();
        throw;
      }
    }
  }
}
