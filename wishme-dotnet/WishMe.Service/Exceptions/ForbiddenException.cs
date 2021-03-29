using System;

namespace WishMe.Service.Exceptions
{
  public class ForbiddenException: Exception
  {
    public ForbiddenException(string message)
        : base(message) { }
  }
}
