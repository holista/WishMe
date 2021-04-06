using MediatR;
using WishMe.Service.Models;

namespace WishMe.Service.Requests
{
  public abstract class GetManyRequestBase<TModel>: IRequest<ListModel<TModel>>
  {
    public QueryModel Model { get; set; } = default!;
  }
}
