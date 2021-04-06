using Microsoft.AspNetCore.Http;

namespace WishMe.Service.Exceptions
{
  public class ConflictException: StatusExceptionBase
  {
    public override int StatusCode => StatusCodes.Status409Conflict;

    public ConflictException(string message)
      : base(message) { }
  }
}
