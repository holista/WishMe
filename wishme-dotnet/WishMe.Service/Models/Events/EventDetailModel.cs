using System;

namespace WishMe.Service.Models.Events
{
  public class EventDetailModel: EventPreviewModel
  {
    public string? Description { get; set; }

    public DateTime UpdatedUtc { get; set; }
  }
}
