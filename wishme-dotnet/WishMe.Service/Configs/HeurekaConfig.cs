using System;

namespace WishMe.Service.Configs
{
  public class HeurekaConfig: IHeurekaConfig
  {
    public string DescriptionClassName { get; } = Environment.GetEnvironmentVariable(EnvVariables._HeurekaDescription)!;
    public string GalleryThumbnailImageClassName { get; } = Environment.GetEnvironmentVariable(EnvVariables._HeurekaGalleryThumbnailImage)!;
    public string RecommendedOfferClassName { get; } = Environment.GetEnvironmentVariable(EnvVariables._HeurekaRecommendedOffer)!;
    public string SearchRequestUrl { get; } = Environment.GetEnvironmentVariable(EnvVariables._HeurekaSearchRequestURL)!;
  }
}