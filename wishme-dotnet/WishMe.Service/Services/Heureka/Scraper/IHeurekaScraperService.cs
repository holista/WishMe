using System.Threading;
using System.Threading.Tasks;
using WishMe.Service.Models.ItemSuggestions.Heureka;

namespace WishMe.Service.Services.Heureka.Scraper
{
  public interface IHeurekaScraperService
  {
    Task<DetailModel?> GetDetailSuggestionAsync(string url, CancellationToken cancellationToken);
  }
}
