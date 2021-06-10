using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WishMe.Service.Configs;
using WishMe.Service.Microsoft.Extensions.DependencyInjection;
using WishMe.Service.Middlewares;

namespace WishMe.Service
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<MongoConfig>();

      services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(AuthenticationConfig.SetupJwtBearer);

      services.AddAuthorization(AuthorizationConfig.SetupRoles);

      services.AddMvc(MvcConfig.SetupMvc)
        .AddNewtonsoftJson(MvcConfig.SetupJsonOptions);

      services.AddControllers();

      services.AddSwaggerGen(SwaggerConfig.SetupSwaggerGen);

      services.AddMediatR(Assembly.GetExecutingAssembly());

      services.AddCors();

      services.AddServices<Startup>();
      services.AddMemoryCache();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseRouting();
      app.UseCors(CorsConfig.Setup);

      app.UseMiddleware<ExceptionMiddleware>();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseSwagger(SwaggerConfig.SetupSwagger);

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
