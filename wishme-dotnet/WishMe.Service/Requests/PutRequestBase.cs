using MediatR;

namespace WishMe.Service.Requests
{
  public class PutRequestBase<TModel>: IRequest
  {
    public int Id { get; set; }

    public TModel Model { get; set; } = default!;
  }
}
