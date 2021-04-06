using MediatR;
using WishMe.Service.Models.Login;

namespace WishMe.Service.Requests.Login
{
  public class RegisterOrganizerRequest: IRequest<LoginOrganizerResponseModel>
  {
    public LoginOrganizerModel Model { get; set; } = default!;
  }
}
