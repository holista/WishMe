using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using WishMe.Service.Exceptions.Handlers;

namespace WishMe.Service.Exceptions.Factories
{
  public class StatusFactory: IStatusFactory
  {
    private readonly IServiceProvider fServiceProvider;

    public StatusFactory(IServiceProvider serviceProvider)
    {
      fServiceProvider = serviceProvider;
    }

    public void Create(Exception exception, out int statusCode, out string message)
    {
      var handler = fServiceProvider
          .GetServices<IExceptionHandler>()
          .SingleOrDefault(x => x.CanHandle(exception));

      if (handler is null)
      {
        statusCode = StatusCodes.Status500InternalServerError;
        message = exception.Message;
        return;
      }

      handler.Handle(exception, out statusCode, out message);
    }
  }
}
