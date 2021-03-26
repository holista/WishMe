using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WishMe.Service.Entities
{
  [Table("Events")]
  public class Event: NamedEntityBase, IAccessibleEntity
  {
    public DateTime DateTimeUtc { get; set; }

    public byte[]? Image { get; set; }

    public int AccessHolderId { get; set; }

    [ForeignKey(nameof(AccessHolderId))]
    public AccessHolder AccessHolder { get; set; } = default!;

    [InverseProperty(nameof(Wishlist.Event))]
    public ICollection<Wishlist> Wishlists { get; set; } = new HashSet<Wishlist>();

    public int OrganizerId { get; set; }

    [ForeignKey(nameof(OrganizerId))]
    public Organizer Organizer { get; set; } = default!;
  }
}
