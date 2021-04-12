using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Text;
using WishMe.Service.Configs;
using WishMe.Service.Models.ItemSuggestions.Heureka;

namespace WishMe.Service.Services
{
  public class HeurekaService: IHeurekaService
  {
    private readonly IHeurekaConfig fHeurekaConfig;

    public HeurekaService(IHeurekaConfig heurekaConfig)
    {
      fHeurekaConfig = heurekaConfig;
    }

    public async Task<DetailModel?> GetDetailSuggestionAsync(string url, CancellationToken cancellationToken)
    {
      try
      {
        var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
        var document = await context.OpenAsync(url, cancellationToken);
        if (document is null)
          return null;

        var image = (IHtmlImageElement)document.Body
          .GetElementsByClassName(fHeurekaConfig.GalleryThumbnailImageClassName)
          .First();

        var price = (IHtmlSpanElement)document.Body
          .GetElementsByClassName(fHeurekaConfig.RecommendedOfferClassName)
          .First();

        var priceNumbers = string
          .Join("", price.TextContent
            .Trim()
            .TakeWhile(ch => !ch.IsLetter())
            .Where(ch => ch.IsDigit()));

        return new DetailModel
        {
          Name = image.AlternativeText,
          ImageUrl = image.Source,
          Price = decimal.Parse(priceNumbers)
        };
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return null;
      }
    }
  }
}