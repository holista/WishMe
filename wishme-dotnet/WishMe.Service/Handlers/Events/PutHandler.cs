using System;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MongoDB.Bson;
using WishMe.Service.Entities;
using WishMe.Service.Exceptions;
using WishMe.Service.Models.Events;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Events;

namespace WishMe.Service.Handlers.Events
{
  public class PutHandler: PutHandlerBase<PutRequest, Event, EventProfileModel>
  {
    public PutHandler(IGenericRepository genericRepository)
      : base(genericRepository) { }

    protected override void DoCheckModel(EventProfileModel model)
    {
      if (model.DateTimeUtc.Kind != DateTimeKind.Utc)
        throw new BadRequestException($"Time of the event '{model.DateTimeUtc}' is not in the UTC format");

      if (model.DateTimeUtc <= DateTime.UtcNow)
        throw new BadRequestException($"Time of the event '{model.DateTimeUtc}' is in the past.");
    }

    protected override async Task<Event> DoCreateUpdatedEntityAsync(ObjectId id, EventProfileModel model, CancellationToken cancellationToken)
    {
      var @event = await fGenericRepository.GetAsync<Event>(id, cancellationToken);
      if (@event is null)
        throw new NotFoundException($"Entity of type '{typeof(Event)}' with ID '{id}' was not found.");

      var updated = model.Adapt<Event>();

      updated.OrganizerId = @event.OrganizerId;

      return updated;
    }
  }
}
