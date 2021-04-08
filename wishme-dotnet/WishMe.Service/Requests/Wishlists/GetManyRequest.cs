using WishMe.Service.Models.Wishlists;

namespace WishMe.Service.Requests.Wishlists
{
  public class GetManyRequest: GetManyRequestBase<WishlistPreviewModel>
  {
    public int EventId { get; set; }
  }
}
