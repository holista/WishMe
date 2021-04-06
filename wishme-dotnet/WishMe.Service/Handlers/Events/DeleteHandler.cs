using WishMe.Service.Entities;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Events;

namespace WishMe.Service.Handlers.Events
{
  public class DeleteHandler: DeleteHandlerBase<DeleteRequest, Event>
  {
    public DeleteHandler(IGenericRepository genericRepository)
      : base(genericRepository) { }
  }
}
