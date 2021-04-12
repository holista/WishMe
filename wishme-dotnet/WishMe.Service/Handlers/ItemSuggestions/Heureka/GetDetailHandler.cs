using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WishMe.Service.Exceptions;
using WishMe.Service.Models.ItemSuggestions.Heureka;
using WishMe.Service.Requests.ItemSuggestions.Heureka;
using WishMe.Service.Services;

namespace WishMe.Service.Handlers.ItemSuggestions.Heureka
{
  public class GetDetailHandler: IRequestHandler<GetDetailRequest, DetailModel>
  {
    private readonly IHeurekaService fHeurekaService;

    public GetDetailHandler(IHeurekaService heurekaService)
    {
      fHeurekaService = heurekaService;
    }

    public async Task<DetailModel> Handle(GetDetailRequest request, CancellationToken cancellationToken)
    {
      var model = await fHeurekaService.GetDetailSuggestionAsync(request.Url, cancellationToken);
      if (model is null)
        throw new BadRequestException($"URL '{request.Url}' does not contain a Heureka item specification.");

      return model;
    }
  }
}
