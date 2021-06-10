using MongoDB.Bson;

namespace WishMe.Service.Models.Login
{
  public class LoginParticipantResponseModel: LoginResponseModel
  {
    public ObjectId EventId { get; set; }
  }
}
