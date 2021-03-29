using Microsoft.AspNetCore.Builder;
using WishMe.Service.Middlewares;

namespace WishMe.Service.Exceptions
{
  public static class ApplicationBuilderExtensions
  {
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
    {
      return app.UseMiddleware<ExceptionMiddleware>();
    }
  }
}
