using System.Collections.Generic;

namespace WishMe.Service.Models
{
  public class ListModel<TModel>
  {
    public int TotalCount { get; set; }

    public List<TModel> Models { get; set; } = default!;
  }
}
