using MongoDB.Driver;

namespace WishMe.Service.Database
{
    public interface IDbContext
    {
        IDbConfig Config { get; }
        IMongoDatabase Database { get; }
    }
}
