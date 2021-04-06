using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WishMe.Service.Attributes;

namespace WishMe.Service.Entities
{
  [Table("AccessHolders")]
  public class AccessHolder: EntityBase
  {
    [Indexed]
    public string Code { get; set; } = default!;

    [InverseProperty(nameof(Entities.Event.AccessHolder))]
    public Event Event { get; set; } = default!;

    [InverseProperty(nameof(Wishlist.AccessHolder))]
    public ICollection<Wishlist> Wishlists { get; set; } = new HashSet<Wishlist>();

    [InverseProperty(nameof(Item.AccessHolder))]
    public ICollection<Item> Items { get; set; } = new HashSet<Item>();
  }
}
