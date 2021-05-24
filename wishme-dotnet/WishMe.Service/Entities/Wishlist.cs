using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WishMe.Service.Entities
{
  [Table("Wishlists")]
  public class Wishlist: EntityBase
  {
    public int EventId { get; set; }

    [ForeignKey(nameof(EventId))]
    public Event Event { get; set; } = default!;

    [InverseProperty(nameof(Item.Wishlist))]
    public ICollection<Item> Items { get; set; } = new HashSet<Item>();
  }
}
