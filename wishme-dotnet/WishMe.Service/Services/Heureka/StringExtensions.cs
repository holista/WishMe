using System.Linq;
using AngleSharp.Text;

namespace WishMe.Service.Services.Heureka
{
  public static class StringExtensions
  {
    public static decimal GetPrice(this string priceElementContent)
    {
      return decimal.Parse(string
        .Join("", priceElementContent
          .Trim()
          .TakeWhile(ch => !ch.IsLetter())
          .Where(ch => ch.IsDigit())));
    }
  }
}
