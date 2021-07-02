namespace WishMe.Service.Configs
{
  public interface IHeurekaConfig
  {
    string GalleryThumbnailImageClassName { get; }
    string RecommendedOfferClassName { get; }
    string DescriptionClassName { get; }
    string SearchRequestUrl { get; }
  }
}
