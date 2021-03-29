using System;

namespace WishMe.Service.Exceptions.Handlers
{
  public interface IExceptionHandler
  {
    bool CanHandle(Exception exception);
    void Handle(Exception exception, out int statusCode, out string message);
  }
}
