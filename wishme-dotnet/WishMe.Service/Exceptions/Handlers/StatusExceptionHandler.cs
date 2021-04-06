using System;
using Microsoft.AspNetCore.Http;

namespace WishMe.Service.Exceptions.Handlers
{
  public class StatusExceptionHandler: IStatusExceptionHandler
  {
    public void Handle(Exception exception, out int statusCode, out string message)
    {
      if (exception is StatusExceptionBase statusException)
        statusCode = statusException.StatusCode;
      else
        statusCode = StatusCodes.Status500InternalServerError;

      message = exception.Message;
    }
  }
}
