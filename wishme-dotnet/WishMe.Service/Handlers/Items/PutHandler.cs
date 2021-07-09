using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MongoDB.Bson;
using WishMe.Service.Entities;
using WishMe.Service.Exceptions;
using WishMe.Service.Models.Items;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Items;

namespace WishMe.Service.Handlers.Items
{
  public class PutHandler: PutHandlerBase<PutRequest, Item, ItemProfileModel>
  {
    public PutHandler(IGenericRepository genericRepository)
      : base(genericRepository) { }

    protected override async Task<Item> DoCreateUpdatedEntityAsync(ObjectId id, ItemProfileModel model, CancellationToken cancellationToken)
    {
      var item = await fGenericRepository.GetAsync<Item>(id, cancellationToken);
      if (item is null)
        throw new NotFoundException($"Entity of type '{typeof(Item)}' with ID '{id}' was not found.");

      var updated = model.Adapt<Item>();

      updated.EventId = item.EventId;
      updated.WishlistId = item.WishlistId;

      return item;
    }
  }
}
