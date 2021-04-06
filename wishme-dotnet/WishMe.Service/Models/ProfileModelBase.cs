namespace WishMe.Service.Models
{
  public abstract class ProfileModelBase
  {
    public string Name { get; set; } = default!;

    public string? Description { get; set; }
  }
}
