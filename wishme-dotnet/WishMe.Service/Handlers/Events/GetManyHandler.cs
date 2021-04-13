using WishMe.Service.Dtos;
using WishMe.Service.Entities;
using WishMe.Service.Models.Events;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Events;
using WishMe.Service.Services;
using WishMe.Service.Services.Identity;

namespace WishMe.Service.Handlers.Events
{
  public class GetManyHandler: GetManyHandlerBase<GetManyRequest, Event, EventPreviewModel>
  {
    public GetManyHandler(
      IGenericRepository genericRepository,
      IIdentityService identityService)
      : base(genericRepository, identityService) { }

    protected override FilterDto<Event> DoCreateFilter(GetManyRequest request)
    {
      return new()
      {
        Offset = request.Model.Offset,
        Limit = request.Model.Limit,
        Filter = @event => @event.OrganizerId == request.OrganizerId,
      };
    }
  }
}
