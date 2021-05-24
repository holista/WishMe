using System.Threading;
using System.Threading.Tasks;
using WishMe.Service.Entities;
using WishMe.Service.Exceptions;
using WishMe.Service.Models.Items;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Items;

namespace WishMe.Service.Handlers.Items
{
  public class PostHandler: PostHandlerBase<PostRequest, Item, ItemProfileModel>
  {
    public PostHandler(IGenericRepository genericRepository)
      : base(genericRepository) { }

    protected override async Task DoCheckModelAsync(PostRequest request, CancellationToken cancellationToken)
    {
      if (!await fGenericRepository.ExistsAsync<Wishlist>(request.WishlistId, cancellationToken))
        throw new NotFoundException($"Wishlist with ID '{request.WishlistId}' was not found.");
    }

    protected override async Task DoSetAdditionalPropertiesAsync(PostRequest request, Item entity, CancellationToken cancellationToken)
    {
      entity.WishlistId = request.WishlistId;

      var wishlist = await fGenericRepository.GetAsync<Wishlist>(request.WishlistId, cancellationToken);

      entity.EventId = wishlist!.EventId;
    }
  }
}
