using System;

namespace WishMe.Service.Exceptions
{
  public class BadRequestException: Exception
  {
    public BadRequestException(string message) : base(message)
    {
    }
  }
}
