using Microsoft.AspNetCore.Http;

namespace WishMe.Service.Exceptions.Handlers
{
  public class UnauthorizedExceptionHandler: ExceptionHandlerBase<UnauthorizedException>
  {
    protected override int DoGetStatusCode(UnauthorizedException exception)
    {
      return StatusCodes.Status401Unauthorized;
    }
  }
}
