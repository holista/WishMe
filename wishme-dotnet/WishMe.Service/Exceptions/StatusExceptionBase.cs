using System;

namespace WishMe.Service.Exceptions
{
  public abstract class StatusExceptionBase: Exception
  {
    public abstract int StatusCode { get; }

    protected StatusExceptionBase(string message)
      : base(message) { }
  }
}
