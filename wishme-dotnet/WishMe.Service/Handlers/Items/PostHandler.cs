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

    protected override void DoSetAdditionalProperties(PostRequest request, Item entity)
    {
      base.DoSetAdditionalProperties(request, entity);

      entity.WishlistId = request.WishlistId;
    }

    protected override async Task<int> DoFetchAccessHolderIdAsync(PostRequest request, CancellationToken cancellationToken)
    {
      var wishlist = await fGenericRepository.GetAsync<Wishlist>(request.WishlistId, cancellationToken);
      if (wishlist is null)
        throw new NotFoundException($"Wishlist with ID '{request.WishlistId}' was not found.");

      return wishlist.AccessHolderId;
    }
  }
}
