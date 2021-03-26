using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WishMe.Service.Entities
{
  [Table("Organizers")]
  public class Organizer: EntityBase
  {
    public string Username { get; set; } = default!;

    public string PasswordHash { get; set; } = default!;

    public string SecuritySalt { get; set; } = default!;

    [InverseProperty(nameof(Event.Organizer))]
    public ICollection<Event> Events { get; set; } = new HashSet<Event>();
  }
}
