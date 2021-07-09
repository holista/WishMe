using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MongoDB.Bson;
using WishMe.Service.Entities;
using WishMe.Service.Exceptions;
using WishMe.Service.Models.Wishlists;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Wishlists;

namespace WishMe.Service.Handlers.Wishlists
{
  public class PutHandler: PutHandlerBase<PutRequest, Wishlist, WishlistProfileModel>
  {
    public PutHandler(IGenericRepository genericRepository)
      : base(genericRepository) { }

    protected override async Task<Wishlist> DoCreateUpdatedEntityAsync(ObjectId id, WishlistProfileModel model, CancellationToken cancellationToken)
    {
      var wishlist = await fGenericRepository.GetAsync<Wishlist>(id, cancellationToken);
      if (wishlist is null)
        throw new NotFoundException($"Entity of type '{typeof(Wishlist)}' with ID '{id}' was not found.");

      var updated = model.Adapt<Wishlist>();

      updated.EventId = wishlist.EventId;

      return updated;
    }
  }
}
