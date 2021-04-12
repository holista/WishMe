namespace WishMe.Service.Models.ItemSuggestions.Heureka
{
  public class DetailModel
  {
    public string Name { get; set; } = default!;

    public string ImageUrl { get; set; } = default!;

    public decimal Price { get; set; }

    public string? Description { get; set; }
  }
}
