using Microsoft.AspNetCore.Http;

namespace _24VSCommon.Exceptions.Handlers
{
    public class ServiceUnavailableExceptionHandler : ExceptionHandlerBase<ServiceUnavailableException>
    {
        protected override int DoGetStatusCode(ServiceUnavailableException exception)
        {
            return StatusCodes.Status503ServiceUnavailable;
        }
    }
}
