using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WishMe.Service.Configs;
using WishMe.Service.Entities;
using WishMe.Service.Middlewares;

namespace WishMe.Service
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<DataContext>(DbConfig.SetupDatabase);

      services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(AuthenticationConfig.SetupJwtBearer);

      services.AddAuthorization(AuthorizationConfig.SetupRoles);

      services.AddControllers()
        .AddNewtonsoftJson(MvcConfig.SetupJsonOptions);

      services.AddSwaggerGen(SwaggerConfig.SetupSwaggerGen);

      services.AddMediatR(Assembly.GetExecutingAssembly());

      services.AddCors();

      services.AddServices<Startup>();
      services.AddMemoryCache();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      using (var serviceScope = app.ApplicationServices.CreateScope())
      {
        serviceScope
          .ServiceProvider.GetRequiredService<DataContext>()
          .Database.Migrate();
      }

      app.UseRouting();
      app.UseCors(CorsConfig.Setup);

      app.UseMiddleware<ExceptionMiddleware>();
      app.UseMiddleware<DbTransactionMiddleware>();

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
