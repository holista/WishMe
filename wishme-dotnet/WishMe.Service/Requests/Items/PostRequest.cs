using WishMe.Service.Models.Items;

namespace WishMe.Service.Requests.Items
{
  public class PostRequest: PostRequestBase<ItemProfileModel>
  {
    public int WishlistId { get; set; }
  }
}
