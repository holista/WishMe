using Microsoft.AspNetCore.Http;

namespace WishMe.Service.Exceptions.Handlers
{
  public class BadRequestExceptionHandler: ExceptionHandlerBase<BadRequestException>
  {
    protected override int DoGetStatusCode(BadRequestException exception)
    {
      return StatusCodes.Status400BadRequest;
    }
  }
}
