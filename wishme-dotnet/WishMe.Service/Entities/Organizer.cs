using WishMe.Service.Database;

namespace WishMe.Service.Entities
{
  [Collection(nameof(DbCollections.Organizers))]
  public class Organizer: DbDocBase
  {
    public string Username { get; set; } = default!;

    public string PasswordHash { get; set; } = default!;

    public string SecuritySalt { get; set; } = default!;
  }
}
