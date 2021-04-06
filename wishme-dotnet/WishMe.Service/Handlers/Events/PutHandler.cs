using System;
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
  }
}
