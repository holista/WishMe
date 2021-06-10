using MongoDB.Bson;
using WishMe.Service.Models.Wishlists;

namespace WishMe.Service.Requests.Wishlists
{
  public class PostRequest: PostRequestBase<WishlistProfileModel>
  {
    public ObjectId EventId { get; set; }
  }
}
