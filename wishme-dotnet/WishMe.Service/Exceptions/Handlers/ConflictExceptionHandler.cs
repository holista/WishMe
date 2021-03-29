using Microsoft.AspNetCore.Http;

namespace WishMe.Service.Exceptions.Handlers
{
  public class ConflictExceptionHandler: ExceptionHandlerBase<ConflictException>
  {
    protected override int DoGetStatusCode(ConflictException exception)
    {
      return StatusCodes.Status409Conflict;
    }
  }
}
