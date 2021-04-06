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
      await base.DoCheckModelAsync(request, cancellationToken);

      if (!await fGenericRepository.ExistsAsync<Event>(request.EventId, cancellationToken))
        throw new NotFoundException($"Event with ID '{request.EventId}' was not found.");
    }

    protected override void DoSetAdditionalProperties(PostRequest request, Wishlist entity)
    {
      base.DoSetAdditionalProperties(request, entity);

      entity.EventId = request.EventId;
    }

    protected override async Task<int> DoFetchAccessHolderIdAsync(PostRequest request, CancellationToken cancellationToken)
    {
      var @event = await fGenericRepository.GetAsync<Event>(request.EventId, cancellationToken);
      if (@event is null)
        throw new NotFoundException($"Event with ID '{request.EventId}' was not found.");

      return @event.AccessHolderId;
    }
  }
}
