using Microsoft.AspNetCore.Http;

namespace WishMe.Service.Exceptions.Handlers
{
  public class ForbiddenExceptionHandler: ExceptionHandlerBase<ForbiddenException>
  {
    protected override int DoGetStatusCode(ForbiddenException exception)
    {
      return StatusCodes.Status403Forbidden;
    }
  }
}
