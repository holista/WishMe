using MongoDB.Bson;

namespace WishMe.Service.Database
{
    public class DbDoc : IDbObject
    {
        public ObjectId Id { get; set; }
    }
}
