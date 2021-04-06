using MediatR;
using WishMe.Service.Models.Login;

namespace WishMe.Service.Requests.Login
{
  public class LoginOrganizerRequest: IRequest<LoginResponseModel>
  {
    public LoginOrganizerModel Model { get; set; } = default!;
  }
}
