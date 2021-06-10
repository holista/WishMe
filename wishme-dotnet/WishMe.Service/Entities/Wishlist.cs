using MongoDB.Bson;
using WishMe.Service.Database;

namespace WishMe.Service.Entities
{
  [Collection(nameof(DbCollections.Wishlists))]
  public class Wishlist: NamedDbDocBase
  {
    public ObjectId EventId { get; set; }
  }
}
