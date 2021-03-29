using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WishMe.Service.Configs
{
  public class MvcConfig
  {
    public static void SetupJsonOptions(MvcNewtonsoftJsonOptions options)
    {
      options.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
      options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    }
  }
}
