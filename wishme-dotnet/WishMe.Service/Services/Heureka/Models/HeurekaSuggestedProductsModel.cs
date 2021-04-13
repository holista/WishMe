namespace WishMe.Service.Services.Heureka.Models
{
  public class HeurekaSuggestedProductsModel
  {
    public HeurekaProductModel[] Result { get; set; } = default!;

    public int Count { get; set; }
  }
}
