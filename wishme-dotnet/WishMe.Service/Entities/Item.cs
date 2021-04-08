using System.ComponentModel.DataAnnotations.Schema;
using WishMe.Service.Attributes;

namespace WishMe.Service.Entities
{
  [Table("Items")]
  public class Item: EntityBase
  {
    public int EventId { get; set; }

    [ForeignKey(nameof(EventId))]
    public Event Event { get; set; } = default!;

    [Indexed]
    public int WishlistId { get; set; }

    [ForeignKey(nameof(WishlistId))]
    public Wishlist Wishlist { get; set; } = default!;

    public decimal Price { get; set; }

    public string? Url { get; set; }

    public string? ImageUrl { get; set; }

    public bool Claimed { get; set; }
  }
}
