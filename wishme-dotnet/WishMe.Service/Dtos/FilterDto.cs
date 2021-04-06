using System;
using System.Linq.Expressions;

namespace WishMe.Service.Dtos
{
  public class FilterDto<TEntity>
  {
    public int Offset { get; set; }

    public int Limit { get; set; }

    public Expression<Func<TEntity, bool>> Filter { get; set; } = default!;

    public SortingKeyDto[]? SortingKeys { get; set; }
  }
}
