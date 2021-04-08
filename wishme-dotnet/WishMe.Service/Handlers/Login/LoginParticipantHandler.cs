using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WishMe.Service.Entities;
using WishMe.Service.Exceptions;
using WishMe.Service.Models.Login;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Login;
using WishMe.Service.Services;

namespace WishMe.Service.Handlers.Login
{
  public class LoginParticipantHandler: IRequestHandler<LoginParticipantRequest, LoginResponseModel>
  {
    private readonly IIdentityService fIdentityService;
    private readonly IGenericRepository fGenericRepository;

    public LoginParticipantHandler(
      IIdentityService identityService,
      IGenericRepository genericRepository)
    {
      fIdentityService = identityService;
      fGenericRepository = genericRepository;
    }

    public async Task<LoginResponseModel> Handle(LoginParticipantRequest request, CancellationToken cancellationToken)
    {
      var @event = await fGenericRepository.GetAsync<Event>(row => row.AccessCode == request.Model.AccessCode, cancellationToken);
      if (@event is null)
        throw new NotFoundException($"Event with access code '{request.Model.AccessCode}' was not found.");

      var model = fIdentityService.Login(@event.AccessCode);

      model.EventId = @event.Id;

      return model;
    }
  }
}
