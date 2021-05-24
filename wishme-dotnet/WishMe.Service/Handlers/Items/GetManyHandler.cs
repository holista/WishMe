using System.Threading;
using System.Threading.Tasks;
using WishMe.Service.Dtos;
using WishMe.Service.Entities;
using WishMe.Service.Exceptions;
using WishMe.Service.Models.Items;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Items;
using WishMe.Service.Services;
using WishMe.Service.Services.Identity;

namespace WishMe.Service.Handlers.Items
{
  public class GetManyHandler: GetManyHandlerBase<GetManyRequest, Item, ItemPreviewModel>
  {
    public GetManyHandler(
      IGenericRepository genericRepository,
      IIdentityService identityService)
      : base(genericRepository, identityService) { }

    protected override async Task CheckAccessibilityAsync(GetManyRequest request, CancellationToken cancellationToken)
    {
      var wishlist = await fGenericRepository.GetAsync<Wishlist>(request.WishlistId, new[] { nameof(Wishlist.Event) }, cancellationToken);
      if (wishlist is null)
        throw new NotFoundException($"Wishlist with ID '{request.WishlistId}' was not found.");

      if (!fIdentityService.CanAccess(wishlist.Event))
        throw new ForbiddenException($"Current user cannot access wishlist with ID '{request.WishlistId}'.");
    }

    protected override FilterDto<Item> DoCreateFilter(GetManyRequest request)
    {
      return new()
      {
        Offset = request.Model.Offset,
        Limit = request.Model.Limit,
        Filter = item => item.WishlistId == request.WishlistId
      };
    }
  }
}
