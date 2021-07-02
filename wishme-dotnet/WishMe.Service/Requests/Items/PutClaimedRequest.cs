using MediatR;
using MongoDB.Bson;
using WishMe.Service.Models.Items;

namespace WishMe.Service.Requests.Items
{
  public class PutClaimedRequest: IRequest
  {
    public ObjectId Id { get; set; }

    public ItemClaimedModel Model { get; set; } = default!;
  }
}
