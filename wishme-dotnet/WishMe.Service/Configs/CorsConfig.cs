using Microsoft.AspNetCore.Cors.Infrastructure;

namespace WishMe.Service.Configs
{
  public class CorsConfig
  {
    public static void Setup(CorsPolicyBuilder options)
    {
      options
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    }
  }
}
