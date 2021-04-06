using WishMe.Service.Entities;
using WishMe.Service.Models.Items;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Items;
using WishMe.Service.Services;

namespace WishMe.Service.Handlers.Items
{
  public class GetHandler: GetHandlerBase<GetRequest, Item, ItemDetailModel>
  {
    public GetHandler(
      IGenericRepository genericRepository,
      IIdentityService identityService)
      : base(genericRepository, identityService) { }
  }
}
