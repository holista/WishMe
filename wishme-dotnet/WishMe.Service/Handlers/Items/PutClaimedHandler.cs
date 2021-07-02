using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using WishMe.Service.Entities;
using WishMe.Service.Exceptions;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Items;
using WishMe.Service.Services.Identity;

namespace WishMe.Service.Handlers.Items
{
  public class PutClaimedHandler: IRequestHandler<PutClaimedRequest>
  {
    private readonly IGenericRepository fGenericRepository;
    private readonly IIdentityService fIdentityService;

    public PutClaimedHandler(
      IGenericRepository genericRepository,
      IIdentityService identityService)
    {
      fGenericRepository = genericRepository;
      fIdentityService = identityService;
    }

    public async Task<Unit> Handle(PutClaimedRequest request, CancellationToken cancellationToken)
    {
      var item = await fGenericRepository.GetAsync<Item>(request.Id, cancellationToken);
      if (item is null)
        throw new NotFoundException($"Entity of type '{typeof(Item)}' with ID '{request.Id}' was not found.");

      var @event = await fGenericRepository.GetAsync<Event>(e => e.Id == item.EventId, cancellationToken);
      if (@event is null)
        throw new NotFoundException($"Event was not found for entity of type '{typeof(Item)}' with ID '{request.Id}'.");

      if (!fIdentityService.CanAccess(@event))
        throw new ForbiddenException($"Current user does not have permissions to view resource with ID '{request.Id}'.");

      var update = Builders<Item>.Update
        .Set(doc => doc.Claimed, request.Model.Claimed);

      await fGenericRepository.UpdateAsync(request.Id, update, cancellationToken);

      return Unit.Value;
    }
  }
}
