using Microsoft.AspNetCore.Http;

namespace WishMe.Service.Exceptions
{
  public class ForbiddenException: StatusExceptionBase
  {
    public override int StatusCode => StatusCodes.Status403Forbidden;

    public ForbiddenException(string message)
        : base(message) { }
  }
}
