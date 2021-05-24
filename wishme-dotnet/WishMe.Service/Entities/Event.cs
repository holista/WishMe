using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WishMe.Service.Attributes;

namespace WishMe.Service.Entities
{
  [Table("Events")]
  public class Event: EntityBase
  {
    public DateTime DateTimeUtc { get; set; }

    public byte[]? Image { get; set; }

    [Indexed]
    public string AccessCode { get; set; } = default!;

    [InverseProperty(nameof(Wishlist.Event))]
    public ICollection<Wishlist> Wishlists { get; set; } = new HashSet<Wishlist>();

    public int OrganizerId { get; set; }

    [ForeignKey(nameof(OrganizerId))]
    public Organizer Organizer { get; set; } = default!;
  }
}
