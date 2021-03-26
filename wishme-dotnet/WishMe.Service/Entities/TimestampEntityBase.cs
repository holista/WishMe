using System;
using System.ComponentModel.DataAnnotations;

namespace WishMe.Service.Entities
{
  public class TimestampEntityBase
  {
    protected TimestampEntityBase()
    {
      CreatedUtc = DateTime.UtcNow;
      UpdatedUtc = CreatedUtc;
    }

    [Required]
    public DateTime CreatedUtc { get; set; }

    [Required]
    public DateTime UpdatedUtc { get; set; }
  }
}
