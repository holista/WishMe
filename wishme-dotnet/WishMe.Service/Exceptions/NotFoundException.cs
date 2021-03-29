using System;

namespace WishMe.Service.Exceptions
{
  public class NotFoundException: Exception
  {
    public NotFoundException(string message)
      : base(message) { }
  }
}
