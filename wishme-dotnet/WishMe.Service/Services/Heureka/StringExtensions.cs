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

    public static string FixUrl(this string heurekaUrl)
    {
      const string urlStart = "//";

      return heurekaUrl.StartsWith(urlStart)
        ? heurekaUrl.Replace(urlStart, "http://")
        : heurekaUrl;
    }

    public static string FixDescription(this string heurekaDescription)
    {
      heurekaDescription = heurekaDescription.Trim();

      const string newline = "\n";

      if (!heurekaDescription.Contains(newline))
        return heurekaDescription;

      int index = heurekaDescription.IndexOf(newline);

      return heurekaDescription.Substring(0, index);
    }
  }
}
