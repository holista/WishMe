using System;
using MongoDB.Bson;
using WishMe.Service.Attributes;
using WishMe.Service.Database;

namespace WishMe.Service.Entities
{
  [Collection(nameof(DbCollections.Events))]
  public class Event: NamedDbDocBase
  {
    public DateTime DateTimeUtc { get; set; }

    public byte[]? Image { get; set; }

    [Indexed]
    public string AccessCode { get; set; } = default!;

    public ObjectId OrganizerId { get; set; }
  }
}
