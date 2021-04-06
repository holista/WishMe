using Microsoft.AspNetCore.Http;

namespace WishMe.Service.Exceptions
{
  public class NotFoundException: StatusExceptionBase
  {
    public override int StatusCode => StatusCodes.Status404NotFound;

    public NotFoundException(string message)
      : base(message) { }
  }
}
