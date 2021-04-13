using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WishMe.Service.Exceptions;
using WishMe.Service.Models.ItemSuggestions.Heureka;
using WishMe.Service.Requests.ItemSuggestions.Heureka;
using WishMe.Service.Services.Heureka;

namespace WishMe.Service.Handlers.ItemSuggestions.Heureka
{
  public class GetManyHandler: IRequestHandler<GetManyRequest, List<PreviewModel>>
  {
    private readonly IHeurekaClientService fHeurekaClientService;

    public GetManyHandler(IHeurekaClientService heurekaClientService)
    {
      fHeurekaClientService = heurekaClientService;
    }

    public async Task<List<PreviewModel>> Handle(GetManyRequest request, CancellationToken cancellationToken)
    {
      var models = await fHeurekaClientService.GetPreviewsAsync(request.Term, cancellationToken);
      if (models is null)
        throw new NotFoundException($"No suggestions were found for term '{request.Term}'.");

      return models;
    }
  }
}
