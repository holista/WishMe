using WishMe.Service.Entities;
using WishMe.Service.Models.Wishlists;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Wishlists;

namespace WishMe.Service.Handlers.Wishlists
{
  public class PutHandler: PutHandlerBase<PutRequest, Wishlist, WishlistProfileModel>
  {
    public PutHandler(IGenericRepository genericRepository)
      : base(genericRepository) { }

    protected override void DoCheckModel(WishlistProfileModel model)
    {
    }
  }
}
