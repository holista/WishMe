using WishMe.Service.Entities;
using WishMe.Service.Models.Wishlists;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Wishlists;
using WishMe.Service.Services;

namespace WishMe.Service.Handlers.Wishlists
{
  public class GetHandler: GetHandlerBase<GetRequest, Wishlist, WishlistDetailModel>
  {
    public GetHandler(
      IGenericRepository genericRepository,
      IIdentityService identityService)
      : base(genericRepository, identityService) { }
  }
}
