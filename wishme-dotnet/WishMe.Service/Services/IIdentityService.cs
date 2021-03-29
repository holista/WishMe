using System.Threading;
using System.Threading.Tasks;
using WishMe.Service.Entities;
using WishMe.Service.Models.Login;

namespace WishMe.Service.Services
{
  public interface IIdentityService
  {
    LoginResponseModel? Login(string password, Organizer organizer);
    LoginResponseModel Login(AccessHolder holder);
    Task<bool> IsOrganizerAsync(CancellationToken cancellationToken);
    Task<bool> CanAccessAsync(IAccessibleEntity accessible, CancellationToken cancellationToken);
  }
}
