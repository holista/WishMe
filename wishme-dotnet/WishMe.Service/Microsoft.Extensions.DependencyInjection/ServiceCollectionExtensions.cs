using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WishMe.Service.Attributes;
using WishMe.Service.Database;

namespace WishMe.Service.Microsoft.Extensions.DependencyInjection
{
  public static class ServiceCollectionExtensions
  {
    public static void AddDbContext<TDbConfig>(this IServiceCollection services)
      where TDbConfig : class, IDbConfig
    {
      services.TryAddSingleton<IDbConfig, TDbConfig>();
      services.TryAddSingleton<IDbContext, DbContext>();
    }

    public static void AddServices<T>(this IServiceCollection services)
    {
      services.Scan(opt =>
      {
        opt.FromAssemblyBy<T>();
        opt.AddTypesWithDefaultConventions();
      });

      services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
    }

    private static void Scan(this IServiceCollection services, Action<ScanOptions> setup)
    {
      if (services is null)
        throw new ArgumentNullException(nameof(services));
      if (setup is null)
        throw new ArgumentNullException(nameof(setup));

      var options = new ScanOptions();
      setup(options);

      if (options.Assemblies.Count == 0)
        options.Assemblies.AddLast(Assembly.GetEntryAssembly() ?? throw new NotSupportedException("Unmanaged execution."));

      var classTypes = options.Assemblies
        .SelectMany(a => a.ExportedTypes)
        .Where(type => type.IsClass && !type.IsAbstract);

      foreach (var classType in classTypes)
      {
        foreach (var interfaceType in options.Interfaces)
          if (interfaceType.IsAssignableFrom(classType))
            services.TryAddEnumerable(new ServiceDescriptor(interfaceType, classType, GetLifetime(classType)));
      }

      if (options.UseDefaultConventions)
        ScanDefaultConventions(services, classTypes);
    }

    private static void ScanDefaultConventions(IServiceCollection services, IEnumerable<Type> classTypes)
    {
      foreach (var classType in classTypes)
      {
        var interfaceType = FindDefaultInterface(classType);
        if (interfaceType is null)
          continue;

        services.TryAdd(new ServiceDescriptor(interfaceType, classType, GetLifetime(classType)));
      }
    }

    private static Type? FindDefaultInterface(Type type)
    {
      try
      {
        var interfaceType = type.GetInterface('I' + type.Name);
        if (interfaceType is null)
          return null;

        if (interfaceType.IsGenericType)
          return interfaceType.GetGenericTypeDefinition();

        return interfaceType;
      }
      catch (AmbiguousMatchException)
      {
        return null;
      }
    }

    private static ServiceLifetime GetLifetime(Type type)
    {
      var attribute = type.GetCustomAttribute<LifetimeAttribute>();
      if (attribute is null)
        return ServiceLifetime.Transient;

      return attribute.Lifetime;
    }
  }
}
