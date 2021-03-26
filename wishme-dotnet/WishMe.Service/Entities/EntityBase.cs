using System.ComponentModel.DataAnnotations;

namespace WishMe.Service.Entities
{
  public abstract class EntityBase: TimestampEntityBase
  {
    [Key]
    public int Id { get; set; }
  }
}
