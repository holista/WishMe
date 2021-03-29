using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WishMe.Service.Entities
{
  [Table("Wishlists")]
  public class Wishlist: NamedEntityBase, IAccessibleEntity
  {
    public int EventId { get; set; }

    [ForeignKey(nameof(EventId))]
    public Event Event { get; set; } = default!;

    public int AccessHolderId { get; set; }

    [ForeignKey(nameof(AccessHolderId))]
    public AccessHolder AccessHolder { get; set; } = default!;

    [InverseProperty(nameof(Item.Wishlist))]
    public ICollection<Item> Items { get; set; } = new HashSet<Item>();
  }
}
