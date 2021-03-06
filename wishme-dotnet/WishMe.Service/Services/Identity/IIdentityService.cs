using MongoDB.Bson;
using WishMe.Service.Entities;
using WishMe.Service.Models.Login;

namespace WishMe.Service.Services.Identity
{
  public interface IIdentityService
  {
    bool TryGetOrganizerId(out ObjectId? organizerId);
    Organizer CreateOrganizer(LoginOrganizerModel model);
    LoginOrganizerResponseModel? Login(string password, Organizer organizer);
    LoginParticipantResponseModel Login(string accessCode);
    bool CanAccess(Event @event);
  }
}
