using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WishMe.Service.Attributes;
using WishMe.Service.Configs;
using WishMe.Service.Models.ItemSuggestions.Heureka;
using WishMe.Service.Services.Heureka.Models;

namespace WishMe.Service.Services.Heureka
{
  [Lifetime(ServiceLifetime.Singleton)]
  public class HeurekaClientService: IHeurekaClientService
  {
    private static readonly JsonSerializerSettings fJsonSerializerSettings = new()
    {
      Formatting = Formatting.Indented,
      ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
    };

    private readonly HttpClient fClient;
    private readonly IMemoryCache fMemoryCache;
    private readonly IHeurekaConfig fHeurekaConfig;

    public HeurekaClientService(
      IMemoryCache memoryCache,
      IHeurekaConfig heurekaConfig)
    {
      fClient = new HttpClient();
      fMemoryCache = memoryCache;
      fHeurekaConfig = heurekaConfig;
    }

    public async Task<List<PreviewModel>?> GetPreviewsAsync(string term, CancellationToken cancellationToken)
    {
      return await fMemoryCache.GetOrCreateAsync(term, entry => FetchPreviewsAsync(entry, term, cancellationToken));
    }

    private async Task<List<PreviewModel>?> FetchPreviewsAsync(ICacheEntry entry, string term, CancellationToken cancellationToken)
    {
      try
      {
        var uri = string.Format(fHeurekaConfig.SearchRequestUrl, term);

        var response = await fClient.GetAsync(uri, cancellationToken);

        var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

        var model = JsonConvert.DeserializeObject<HeurekaSearchResponseModel>(responseString, fJsonSerializerSettings);
        if (model is null)
          return null;

        entry.SlidingExpiration = TimeSpan.FromMinutes(5);

        return model.Products.Result
          .Select(m => new PreviewModel
          {
            Name = m.Name,
            ImageUrl = m.ImageUrl,
            Price = m.Price.GetPrice(),
            Url = m.DesktopUrl
          })
          .ToList();
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return null;
      }
    }
  }
}