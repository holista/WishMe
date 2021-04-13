using System;
using WishMe.Service.Entities;
using WishMe.Service.Models.Events;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Events;
using WishMe.Service.Services;
using WishMe.Service.Services.Identity;

namespace WishMe.Service.Handlers.Events
{
  public class GetHandler: GetHandlerBase<GetRequest, Event, EventDetailModel>
  {
    protected override Func<Event, Event> EventSelector => @event => @event;

#warning todle je shit, organizera nepotrebuju
    protected override string Include => nameof(Event.Organizer);

    public GetHandler(
      IGenericRepository genericRepository,
      IIdentityService identityService)
      : base(genericRepository, identityService) { }
  }
}
