namespace WishMe.Service.Models
{
  public class LoginResponseModel
  {
    public string Token { get; set; } = default!;

    public int ExpirationUtc { get; set; }
  }
}
