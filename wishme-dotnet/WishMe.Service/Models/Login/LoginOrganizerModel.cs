using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WishMe.Service.Models.Login
{
  public class LoginOrganizerModel
  {
    [Required, JsonRequired]
    [MinLength(4)]
    public string Username { get; set; } = default!;

    [Required, JsonRequired]
    [MinLength(5)]
    public string Password { get; set; } = default!;
  }
}
