using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WishMe.Service.Binders;
using WishMe.Service.Database;

namespace WishMe.Service.Configs
{
  public class MvcConfig
  {
    public static void SetupMvc(MvcOptions options)
    {
      options.ModelBinderProviders.Insert(0, new ObjectIdBinderProvider());
    }

    public static void SetupJsonOptions(MvcNewtonsoftJsonOptions options)
    {
      options.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
      options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
      options.SerializerSettings.Converters.Add(new ObjectIdJsonConverter());
    }
  }
}
