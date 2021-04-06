using WishMe.Service.Models.Wishlists;

namespace WishMe.Service.Requests.Wishlists
{
  public class PostRequest: PostRequestBase<WishlistProfileModel>
  {
    public int EventId { get; set; }
  }
}
