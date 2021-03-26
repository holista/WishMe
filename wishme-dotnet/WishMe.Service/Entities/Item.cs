using System.ComponentModel.DataAnnotations.Schema;
using WishMe.Service.Attributes;

namespace WishMe.Service.Entities
{
  [Table("Items")]
  public class Item: NamedEntityBase, IAccessibleEntity
  {
    public int AccessHolderId { get; set; }

    [ForeignKey(nameof(AccessHolderId))]
    public AccessHolder AccessHolder { get; set; } = default!;

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
