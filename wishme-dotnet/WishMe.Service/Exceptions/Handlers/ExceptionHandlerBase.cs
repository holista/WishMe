using System;

namespace WishMe.Service.Exceptions.Handlers
{
  public abstract class ExceptionHandlerBase<TException>: IExceptionHandler
    where TException : Exception
  {
    public bool CanHandle(Exception exception)
    {
      return exception is TException;
    }

    public void Handle(Exception exception, out int statusCode, out string message)
    {
      if (exception == null)
        throw new ArgumentNullException(nameof(exception));

      var specificException = (TException)exception;

      statusCode = DoGetStatusCode(specificException);
      message = DoGetMessage(specificException);
    }

    protected virtual string DoGetMessage(TException exception)
    {
      return exception.Message;
    }

    protected abstract int DoGetStatusCode(TException exception);
  }
}
