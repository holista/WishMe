using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using Microsoft.Extensions.DependencyInjection;
using WishMe.Service.Attributes;
using WishMe.Service.Configs;
using WishMe.Service.Models.ItemSuggestions.Heureka;

namespace WishMe.Service.Services.Heureka.Scraper
{
  [Lifetime(ServiceLifetime.Singleton)]
  public class HeurekaScraperService: IHeurekaScraperService
  {
    private readonly IHeurekaConfig fHeurekaConfig;

    public HeurekaScraperService(IHeurekaConfig heurekaConfig)
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

        return new DetailModel
        {
          Name = image.AlternativeText,
          ImageUrl = image.Source,
          Price = price.TextContent.GetPrice()
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