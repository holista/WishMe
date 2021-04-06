using System;

namespace WishMe.Service.Models.Events
{
  public class EventPreviewModel: PreviewModelBase
  {
    public DateTime DateTimeUtc { get; set; }

    public byte[]? Image { get; set; }
  }
}
