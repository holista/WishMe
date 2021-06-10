using System;
using WishMe.Service.Database;

namespace WishMe.Service.Configs
{
  public class MongoConfig: IDbConfig
  {
    public string Url { get; } = Environment.GetEnvironmentVariable(EnvVariables._MongoUrl)!;

    public string DatabaseName { get; } = Environment.GetEnvironmentVariable(EnvVariables._MongoDbName)!;
  }
}
