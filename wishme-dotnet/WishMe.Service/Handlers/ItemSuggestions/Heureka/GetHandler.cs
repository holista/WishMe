using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WishMe.Service.Exceptions;
using WishMe.Service.Models.ItemSuggestions.Heureka;
using WishMe.Service.Requests.ItemSuggestions.Heureka;
using WishMe.Service.Services.Heureka.Scraper;

namespace WishMe.Service.Handlers.ItemSuggestions.Heureka
{
  public class GetHandler: IRequestHandler<GetRequest, DetailModel>
  {
    private readonly IHeurekaScraperService fHeurekaScraperService;

    public GetHandler(IHeurekaScraperService heurekaScraperService)
    {
      fHeurekaScraperService = heurekaScraperService;
    }

    public async Task<DetailModel> Handle(GetRequest request, CancellationToken cancellationToken)
    {
      var model = await fHeurekaScraperService.GetDetailSuggestionAsync(request.Url, cancellationToken);
      if (model is null)
        throw new BadRequestException($"URL '{request.Url}' does not contain a Heureka item specification.");

      return model;
    }
  }
}
