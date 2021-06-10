using MongoDB.Bson;
using WishMe.Service.Models.Items;

namespace WishMe.Service.Requests.Items
{
  public class PostRequest: PostRequestBase<ItemProfileModel>
  {
    public ObjectId WishlistId { get; set; }
  }
}
