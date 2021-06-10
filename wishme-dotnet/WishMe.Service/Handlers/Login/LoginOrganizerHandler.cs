using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WishMe.Service.Entities;
using WishMe.Service.Exceptions;
using WishMe.Service.Models.Login;
using WishMe.Service.Repositories;
using WishMe.Service.Requests.Login;
using WishMe.Service.Services.Identity;

namespace WishMe.Service.Handlers.Login
{
  public class LoginOrganizerHandler: IRequestHandler<LoginOrganizerRequest, LoginResponseModel>
  {
    private readonly IIdentityService fIdentityService;
    private readonly IGenericRepository fGenericRepository;

    public LoginOrganizerHandler(
      IIdentityService identityService,
      IGenericRepository genericRepository)
    {
      fIdentityService = identityService;
      fGenericRepository = genericRepository;
    }

    public async Task<LoginResponseModel> Handle(LoginOrganizerRequest request, CancellationToken cancellationToken)
    {
      var organizer = await fGenericRepository.GetAsync<Organizer>(row => row.Username == request.Model.Username, cancellationToken);
      if (organizer is null)
        throw new UnauthorizedException($"Username or password is not correct.");

      var model = fIdentityService.Login(request.Model.Password, organizer);
      if (model is null)
        throw new UnauthorizedException($"Username or password is not correct.");

      model.OrganizerId = organizer.Id;

      return model;
    }
  }
}
