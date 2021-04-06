using WishMe.Service.Entities;
using WishMe.Service.Models.Items;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Items;

namespace WishMe.Service.Handlers.Items
{
  public class PutHandler: PutHandlerBase<PutRequest, Item, ItemProfileModel>
  {
    public PutHandler(IGenericRepository genericRepository)
      : base(genericRepository) { }

    protected override void DoCheckModel(ItemProfileModel model)
    {
    }
  }
}
