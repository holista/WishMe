using WishMe.Service.Entities;
using WishMe.Service.Models.Login;

namespace WishMe.Service.Services
{
  public interface IIdentityService
  {
    bool TryGetOrganizerId(out int? organizerId);
    Organizer CreateOrganizer(LoginOrganizerModel model);
    LoginOrganizerResponseModel? Login(string password, Organizer organizer);
    LoginParticipantResponseModel Login(string accessCode);
    bool CanAccess(Event @event);
  }
}
