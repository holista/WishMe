using MediatR;

namespace WishMe.Service.Requests
{
  public abstract class DeleteRequestBase: IRequest
  {
    public int Id { get; set; }
  }
}
