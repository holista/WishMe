using Microsoft.AspNetCore.Http;

namespace WishMe.Service.Exceptions
{
  public class UnauthorizedException: StatusExceptionBase
  {
    public override int StatusCode => StatusCodes.Status401Unauthorized;

    public UnauthorizedException(string message)
      : base(message) { }
  }
}
