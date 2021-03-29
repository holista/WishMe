using System;

namespace WishMe.Service.Extensions
{
  public static class DateTimeExtensions
  {
    public static int ToUnixTimeSeconds(this DateTime dateTimeUtc)
    {
      return (dateTimeUtc - new DateTime(1970, 1, 1)).Seconds;
    }
  }
}
