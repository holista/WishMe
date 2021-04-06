namespace WishMe.Service.Models.Items
{
  public class ItemProfileModel: ProfileModelBase
  {
    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    public string? Url { get; set; }
  }
}
