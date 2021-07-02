namespace WishMe.Service.Configs
{
  public static class EnvVariables
  {
    public const string _EnvVariablePrefix = "WISH_ME_";

    public const string _MongoUrl = _EnvVariablePrefix + "MONGO_URL";
    public const string _MongoDbName = _EnvVariablePrefix + "MONGO_DB_NAME";

    public const string _JwtKey = _EnvVariablePrefix + "JWT_KEY";
    public const string _JwtDebugMode = _EnvVariablePrefix + "JWT_DEBUG_MODE";

    public const string _HeurekaDescription = _EnvVariablePrefix + "HEUREKA_DESCRIPTION";
    public const string _HeurekaGalleryThumbnailImage = _EnvVariablePrefix + "HEUREKA_GALLERY_THUMBNAIL_IMAGE";
    public const string _HeurekaRecommendedOffer = _EnvVariablePrefix + "HEUREKA_RECOMMENDED_OFFER";
    public const string _HeurekaSearchRequestURL = _EnvVariablePrefix + "HEUREKA_SEARCH_REQUEST_URL";
  }
}
