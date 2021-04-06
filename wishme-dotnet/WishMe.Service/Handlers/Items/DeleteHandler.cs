using WishMe.Service.Entities;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Items;

namespace WishMe.Service.Handlers.Items
{
  public class DeleteHandler: DeleteHandlerBase<DeleteRequest, Item>
  {
    public DeleteHandler(IGenericRepository genericRepository)
      : base(genericRepository) { }
  }
}
