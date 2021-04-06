using MediatR;
using WishMe.Service.Models;

namespace WishMe.Service.Requests
{
  public abstract class PostRequestBase<TModel>: IRequest<IdModel>
  {
    public TModel Model { get; set; } = default!;
  }
}
