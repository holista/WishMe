using System.ComponentModel.DataAnnotations.Schema;

namespace WishMe.Service.Entities
{
  public abstract class AccessibleEntityBase: NamedEntityBase, IAccessibleEntity
  {
    public int AccessHolderId { get; set; }

    [ForeignKey(nameof(AccessHolderId))]
    public AccessHolder AccessHolder { get; set; } = default!;
  }
}
