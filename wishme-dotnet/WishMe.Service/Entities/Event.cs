using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WishMe.Service.Entities
{
  [Table("Events")]
  public class Event: AccessibleEntityBase
  {
    public DateTime DateTimeUtc { get; set; }

    public byte[]? Image { get; set; }

    [InverseProperty(nameof(Wishlist.Event))]
    public ICollection<Wishlist> Wishlists { get; set; } = new HashSet<Wishlist>();

    public int OrganizerId { get; set; }

    [ForeignKey(nameof(OrganizerId))]
    public Organizer Organizer { get; set; } = default!;
  }
}
