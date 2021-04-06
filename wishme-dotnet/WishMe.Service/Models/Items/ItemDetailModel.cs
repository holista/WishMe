using System;

namespace WishMe.Service.Models.Items
{
  public class ItemDetailModel: ItemPreviewModel
  {
    public DateTime UpdatedUtc { get; set; }

    public string? Description { get; set; }

    public string? Url { get; set; }
  }
}
