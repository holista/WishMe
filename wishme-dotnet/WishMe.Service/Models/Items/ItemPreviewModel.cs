namespace WishMe.Service.Models.Items
{
  public class ItemPreviewModel: PreviewModelBase
  {
    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    public bool Claimed { get; set; }
  }
}
