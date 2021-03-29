using System;

namespace WishMe.Service.Exceptions.Factories
{
  public interface IStatusFactory
  {
    void Create(Exception exception, out int statusCode, out string message);
  }
}