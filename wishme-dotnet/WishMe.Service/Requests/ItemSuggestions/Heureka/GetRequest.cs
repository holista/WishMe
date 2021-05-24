using MediatR;
using WishMe.Service.Models.ItemSuggestions.Heureka;

namespace WishMe.Service.Requests.ItemSuggestions.Heureka
{
  public class GetRequest: IRequest<DetailModel>
  {
    public string Url { get; set; } = default!;
  }
}
