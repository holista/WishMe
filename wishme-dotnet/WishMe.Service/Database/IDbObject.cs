using MongoDB.Bson;

namespace WishMe.Service.Database
{
    public interface IDbObject
    {
        ObjectId Id { get; set; }
    }
}
