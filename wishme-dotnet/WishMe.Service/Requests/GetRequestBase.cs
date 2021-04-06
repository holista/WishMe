using MediatR;

namespace WishMe.Service.Requests
{
  public abstract class GetRequestBase<TModel>: IRequest<TModel>
  {
    public int Id { get; set; }
  }
}
