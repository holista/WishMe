using System.Threading;
using System.Threading.Tasks;
using WishMe.Service.Entities;
using WishMe.Service.Exceptions;
using WishMe.Service.Models.Wishlists;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Wishlists;

namespace WishMe.Service.Handlers.Wishlists
{
  public class PostHandler: PostHandlerBase<PostRequest, Wishlist, WishlistProfileModel>
  {
    public PostHandler(IGenericRepository genericRepository)
      : base(genericRepository) { }

    protected override async Task DoCheckModelAsync(PostRequest request, CancellationToken cancellationToken)
    {
      if (!await fGenericRepository.ExistsAsync<Event>(request.EventId, cancellationToken))
        throw new NotFoundException($"Event with ID '{request.EventId}' was not found.");
    }

    protected override Task DoSetAdditionalPropertiesAsync(PostRequest request, Wishlist entity, CancellationToken cancellationToken)
    {
      entity.EventId = request.EventId;

      return Task.CompletedTask;
    }
  }
}
