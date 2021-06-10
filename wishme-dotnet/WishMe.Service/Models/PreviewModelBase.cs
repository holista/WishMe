using MongoDB.Bson;

namespace WishMe.Service.Models
{
  public abstract class PreviewModelBase
  {
    public ObjectId Id { get; set; }

    public string Name { get; set; } = default!;
  }
}
