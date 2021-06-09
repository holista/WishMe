using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace WishMe.Service.Configs
{
  public class DbConfig
  {
    public static string ConnectionString { get; } = Environment.GetEnvironmentVariable(EnvVariables._DbConnectionString)
         ?? "Server=host.docker.internal;Database=WishMe-dev;User Id=SA;Password=Pass@word1";
#warning jen kvuli migraci, smazat pak

    public static void SetupDatabase(DbContextOptionsBuilder options)
    {
#warning tohle je shit, ale jinak to nechce nastartovat spravne pres docker CLI
      Thread.Sleep(3000);

      options.UseSqlServer(ConnectionString, opt =>
      {
        opt.CommandTimeout(60);
      });
    }
  }
}
