namespace WishMe.Service.Configs
{
  public static class EnvVariables
  {
    public static string _EnvVariablePrefix = "WISH_ME_";

    public static string _DbConnectionString = _EnvVariablePrefix + "DB_CONNECTION_STRING";
    public static string _JwtKey = _EnvVariablePrefix + "JWT_KEY";
    public static string _JwtDebugMode = _EnvVariablePrefix + "JWT_DEBUG_MODE";
    public static string _HeurekaGalleryThumbnailImage = _EnvVariablePrefix + "HEUREKA_GALLERY_THUMBNAIL_IMAGE";
    public static string _HeurekaRecommendedOffer = _EnvVariablePrefix + "HEUREKA_RECOMMENDED_OFFER";
  }
}
