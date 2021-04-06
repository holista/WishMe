using WishMe.Service.Entities;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Wishlists;

namespace WishMe.Service.Handlers.Wishlists
{
  public class DeleteHandler: DeleteHandlerBase<DeleteRequest, Wishlist>
  {
    public DeleteHandler(IGenericRepository genericRepository)
      : base(genericRepository) { }
  }
}
