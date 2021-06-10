using MongoDB.Bson;

namespace WishMe.Service.Models.Login
{
  public class LoginOrganizerResponseModel: LoginResponseModel
  {
    public ObjectId OrganizerId { get; set; }
  }
}
