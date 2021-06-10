using MediatR;
using MongoDB.Bson;

namespace WishMe.Service.Requests
{
  public abstract class GetRequestBase<TModel>: IRequest<TModel>
  {
    public ObjectId Id { get; set; }
  }
}
