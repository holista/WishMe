using System;
using WishMe.Service.Entities;
using WishMe.Service.Models.Items;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Items;
using WishMe.Service.Services;

namespace WishMe.Service.Handlers.Items
{
  public class GetHandler: GetHandlerBase<GetRequest, Item, ItemDetailModel>
  {
    protected override Func<Item, Event> EventSelector => item => item.Event;

    protected override string Include => nameof(Item.Event);

    public GetHandler(
      IGenericRepository genericRepository,
      IIdentityService identityService)
      : base(genericRepository, identityService) { }

  }
}
