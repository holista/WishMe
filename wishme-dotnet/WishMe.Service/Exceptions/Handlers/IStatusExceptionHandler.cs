using System;

namespace WishMe.Service.Exceptions.Handlers
{
  public interface IStatusExceptionHandler
  {
    void Handle(Exception exception, out int statusCode, out string message);
  }
}