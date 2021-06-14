using MediatR;
using WishMe.Service.Models.Login;

namespace WishMe.Service.Requests.Login
{
  public class LoginParticipantRequest: IRequest<LoginParticipantResponseModel>
  {
    public LoginParticipantModel Model { get; set; } = default!;
  }
}
