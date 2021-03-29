namespace WishMe.Service.Models.Login
{
  public class LoginResponseModel
  {
    public string Token { get; set; } = default!;

    public int ExpirationUtc { get; set; }
  }
}
