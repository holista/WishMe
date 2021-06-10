using System;
using MongoDB.Driver;

namespace WishMe.Service.Database
{
    public class DbContext : IDbContext
    {
        public IDbConfig Config { get; }

        public IMongoDatabase Database { get; }

        public DbContext(IDbConfig config)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));

            var client = new MongoClient(config.Url);

            Database = client.GetDatabase(config.DatabaseName);
        }
    }
}
