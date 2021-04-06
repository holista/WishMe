using System;

namespace WishMe.Service.Models.Events
{
  public class EventProfileModel: ProfileModelBase
  {
    public DateTime DateTimeUtc { get; set; }

    public byte[]? Image { get; set; }
  }
}
