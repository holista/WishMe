using MediatR;
using MongoDB.Bson;

namespace WishMe.Service.Requests
{
  public abstract class DeleteRequestBase: IRequest
  {
    public ObjectId Id { get; set; }
  }
}
