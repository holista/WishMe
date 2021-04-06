using System.Collections.Generic;

namespace WishMe.Service.Dtos
{
  public class ListDto<TEntity>
  {
    public int TotalCount { get; set; }

    public List<TEntity> Entities { get; set; } = default!;
  }
}
