using WishMe.Service.Models.Items;

namespace WishMe.Service.Requests.Items
{
  public class GetManyRequest: GetManyRequestBase<ItemPreviewModel>
  {
    public int WishlistId { get; set; }
  }
}
