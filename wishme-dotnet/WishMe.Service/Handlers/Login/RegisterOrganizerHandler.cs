using System.Diagnostics;
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
  public class RegisterOrganizerHandler: IRequestHandler<RegisterOrganizerRequest, LoginOrganizerResponseModel>
  {
    private readonly IGenericRepository fGenericRepository;
    private readonly IIdentityService fIdentityService;

    public RegisterOrganizerHandler(
      IGenericRepository genericRepository,
      IIdentityService identityService)
    {
      fGenericRepository = genericRepository;
      fIdentityService = identityService;
    }

    public async Task<LoginOrganizerResponseModel> Handle(RegisterOrganizerRequest request, CancellationToken cancellationToken)
    {
      if (await fGenericRepository.ExistsAsync<Organizer>(doc => doc.Username == request.Model.Username, cancellationToken))
        throw new ConflictException($"User with username '{request.Model.Username}' already exists.");

      var organizer = fIdentityService.CreateOrganizer(request.Model);

      var id = await fGenericRepository.CreateAsync(organizer, cancellationToken);

      var model = fIdentityService.Login(request.Model.Password, organizer);

      Debug.Assert(model != null);

      model.OrganizerId = id;

      return model;
    }
  }
}
