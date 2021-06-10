using MongoDB.Bson;
using WishMe.Service.Models.Wishlists;

namespace WishMe.Service.Requests.Wishlists
{
  public class GetManyRequest: GetManyRequestBase<WishlistPreviewModel>
  {
    public ObjectId EventId { get; set; }
  }
}
