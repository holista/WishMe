using Microsoft.AspNetCore.Http;

namespace WishMe.Service.Exceptions
{
  public class BadRequestException: StatusExceptionBase
  {
    public override int StatusCode => StatusCodes.Status400BadRequest;

    public BadRequestException(string message)
      : base(message) { }
  }
}
