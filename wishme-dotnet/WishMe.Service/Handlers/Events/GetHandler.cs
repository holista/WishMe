using System.Threading;
using System.Threading.Tasks;
using WishMe.Service.Entities;
using WishMe.Service.Models.Events;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Events;
using WishMe.Service.Services.Identity;

namespace WishMe.Service.Handlers.Events
{
  public class GetHandler: GetHandlerBase<GetRequest, Event, EventDetailModel>
  {
    public GetHandler(
      IGenericRepository genericRepository,
      IIdentityService identityService)
      : base(genericRepository, identityService) { }

    protected override async Task<Event?> DoGetEventAsync(Event entity, CancellationToken cancellationToken)
    {
      return entity;
    }
  }
}
