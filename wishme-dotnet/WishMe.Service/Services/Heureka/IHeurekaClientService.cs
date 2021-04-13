using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WishMe.Service.Models.ItemSuggestions.Heureka;

namespace WishMe.Service.Services.Heureka
{
  public interface IHeurekaClientService
  {
    Task<List<PreviewModel>?> GetPreviewsAsync(string term, CancellationToken cancellationToken);
  }
}
