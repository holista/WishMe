using System.Threading;
using System.Threading.Tasks;
using WishMe.Service.Dtos;
using WishMe.Service.Entities;
using WishMe.Service.Exceptions;
using WishMe.Service.Models.Wishlists;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Wishlists;
using WishMe.Service.Services;

namespace WishMe.Service.Handlers.Wishlists
{
  public class GetManyHandler: GetManyHandlerBase<GetManyRequest, Wishlist, WishlistPreviewModel>
  {
    public GetManyHandler(
      IGenericRepository genericRepository,
      IIdentityService identityService)
      : base(genericRepository, identityService) { }

    protected override async Task CheckAccessibilityAsync(GetManyRequest request, CancellationToken cancellationToken)
    {
      var @event = await fGenericRepository.GetAsync<Event>(request.EventId, cancellationToken);
      if (@event is null)
        throw new NotFoundException($"Event with ID '{request.EventId}' was not found.");

      if (!fIdentityService.CanAccess(@event))
        throw new ForbiddenException($"Current user cannot access event with ID '{request.EventId}'.");
    }

    protected override FilterDto<Wishlist> DoCreateFilter(GetManyRequest request)
    {
      return new()
      {
        Offset = request.Model.Offset,
        Limit = request.Model.Limit,
        Filter = wishlist => wishlist.EventId == request.EventId
      };
    }
  }
}
