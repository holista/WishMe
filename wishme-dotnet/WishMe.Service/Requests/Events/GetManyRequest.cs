using WishMe.Service.Models.Events;

namespace WishMe.Service.Requests.Events
{
  public class GetManyRequest: GetManyRequestBase<EventPreviewModel>
  {
    public int OrganizerId { get; set; }
  }
}
