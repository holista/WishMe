namespace WishMe.Service.Models
{
  public abstract class PreviewModelBase
  {
    public int Id { get; set; }

    public string Name { get; set; } = default!;
  }
}
