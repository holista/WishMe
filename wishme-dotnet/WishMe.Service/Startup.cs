using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace WishMe.Service
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddServices<Startup>();
      services.AddMemoryCache();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
    }
  }
}
