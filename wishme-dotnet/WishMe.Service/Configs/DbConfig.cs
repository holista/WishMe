using System;
using Microsoft.EntityFrameworkCore;

namespace WishMe.Service.Configs
{
  public class DbConfig
  {
    public static string ConnectionString { get; } = Environment.GetEnvironmentVariable(EnvVariables._DbConnectionString)
         ?? "Server=host.docker.internal;Database=WishMe-dev;User Id=SA;Password=Pass@word1";
    //#warning jen kvuli migraci, smazat pak

    public static void SetupDatabase(DbContextOptionsBuilder options)
    {
      options.UseSqlServer(ConnectionString, opt =>
      {
        opt.CommandTimeout(60);
      });
    }
  }
}
