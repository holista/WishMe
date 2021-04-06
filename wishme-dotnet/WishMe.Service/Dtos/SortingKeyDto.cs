namespace WishMe.Service.Dtos
{
  public class SortingKeyDto
  {
    public bool Descending { get; set; }

    public string Property { get; set; } = default!;
  }
}
