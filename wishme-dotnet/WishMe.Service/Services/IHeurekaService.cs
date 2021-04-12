using System.Threading;
using System.Threading.Tasks;
using WishMe.Service.Models.ItemSuggestions.Heureka;

namespace WishMe.Service.Services
{
  public interface IHeurekaService
  {
    Task<DetailModel?> GetDetailSuggestionAsync(string url, CancellationToken cancellationToken);
  }
}
