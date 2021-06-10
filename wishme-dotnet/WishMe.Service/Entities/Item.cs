using MongoDB.Bson;
using WishMe.Service.Attributes;
using WishMe.Service.Database;

namespace WishMe.Service.Entities
{
  [Collection(nameof(DbCollections.Items))]
  public class Item: NamedDbDocBase
  {
    public ObjectId EventId { get; set; }

    [Indexed]
    public ObjectId WishlistId { get; set; }

    public decimal Price { get; set; }

    public string? Url { get; set; }

    public string? ImageUrl { get; set; }

    public bool Claimed { get; set; }
  }
}
