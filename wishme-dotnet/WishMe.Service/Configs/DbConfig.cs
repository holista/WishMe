using System;
using Microsoft.EntityFrameworkCore;

namespace WishMe.Service.Configs
{
  public class DbConfig
  {
    public static string ConnectionString { get; } = Environment.GetEnvironmentVariable(EnvVariables._DbConnectionString)!;

    public static void SetupDatabase(DbContextOptionsBuilder options)
    {
      options.UseSqlServer(ConnectionString, opt =>
      {
        opt.CommandTimeout(60);
      });
    }
  }
}
