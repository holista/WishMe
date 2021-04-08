using System;
using WishMe.Service.Entities;
using WishMe.Service.Models.Wishlists;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Wishlists;
using WishMe.Service.Services;

namespace WishMe.Service.Handlers.Wishlists
{
  public class GetHandler: GetHandlerBase<GetRequest, Wishlist, WishlistDetailModel>
  {
    protected override Func<Wishlist, Event> EventSelector => wishlist => wishlist.Event;

    protected override string Include => nameof(Wishlist.Event);

    public GetHandler(
      IGenericRepository genericRepository,
      IIdentityService identityService)
      : base(genericRepository, identityService) { }
  }
}
