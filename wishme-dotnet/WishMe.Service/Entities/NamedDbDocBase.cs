namespace WishMe.Service.Entities
{
  public abstract class NamedDbDocBase: DbDocBase
  {
    public string Name { get; set; } = default!;

    public string? Description { get; set; }
  }
}
