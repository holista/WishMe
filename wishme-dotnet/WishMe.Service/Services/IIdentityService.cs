using System.Threading;
using System.Threading.Tasks;
using WishMe.Service.Entities;
using WishMe.Service.Models.Login;

namespace WishMe.Service.Services
{
  public interface IIdentityService
  {
    Organizer CreateOrganizer(LoginOrganizerModel model);
    LoginOrganizerResponseModel? Login(string password, Organizer organizer);
    LoginParticipantResponseModel Login(AccessHolder holder);
    Task<bool> IsOrganizerAsync(CancellationToken cancellationToken);
    Task<bool> CanAccessAsync(IAccessibleEntity accessible, CancellationToken cancellationToken);
  }
}
