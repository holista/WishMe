namespace WishMe.Service.Entities
{
  public abstract class NamedEntityBase: EntityBase
  {
    public string Name { get; set; } = default!;

    public string? Description { get; set; }
  }
}
