using MediatR;
using WishMe.Service.Models.Login;

namespace WishMe.Service.Requests.Login
{
  public class LoginParticipantRequest: IRequest<LoginResponseModel>
  {
    public LoginParticipantModel Model { get; set; } = default!;
  }
}
