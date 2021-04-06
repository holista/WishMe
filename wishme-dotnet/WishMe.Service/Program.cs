using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WishMe.Service
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args)
        .Build()
        .Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host
        .CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
          webBuilder.UseStartup<Startup>();
        });
    }
  }
}
