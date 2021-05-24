using System.Collections.Generic;
using MediatR;
using WishMe.Service.Models.ItemSuggestions.Heureka;

namespace WishMe.Service.Requests.ItemSuggestions.Heureka
{
  public class GetManyRequest: IRequest<List<PreviewModel>>
  {
    public string Term { get; set; } = default!;
  }
}
