using System.Threading;
using System.Threading.Tasks;
using WishMe.Service.Entities;
using WishMe.Service.Models.Wishlists;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Wishlists;
using WishMe.Service.Services.Identity;

namespace WishMe.Service.Handlers.Wishlists
{
  public class GetHandler: GetHandlerBase<GetRequest, Wishlist, WishlistDetailModel>
  {
    public GetHandler(
      IGenericRepository genericRepository,
      IIdentityService identityService)
      : base(genericRepository, identityService) { }

    protected override async Task<Event?> DoGetEventAsync(Wishlist entity, CancellationToken cancellationToken)
    {
      return await fGenericRepository.GetAsync<Event>(entity.EventId, cancellationToken);
    }
  }
}
