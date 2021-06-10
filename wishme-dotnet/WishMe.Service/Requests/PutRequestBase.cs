using MediatR;
using MongoDB.Bson;

namespace WishMe.Service.Requests
{
  public class PutRequestBase<TModel>: IRequest
  {
    public ObjectId Id { get; set; }

    public TModel Model { get; set; } = default!;
  }
}
