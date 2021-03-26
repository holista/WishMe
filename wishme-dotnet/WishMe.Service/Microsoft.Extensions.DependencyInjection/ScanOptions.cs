using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
  public sealed class ScanOptions
  {
    public ScanOptions()
    {
      Assemblies = new LinkedList<Assembly>();
      Interfaces = new LinkedList<Type>();
    }

    internal LinkedList<Assembly> Assemblies { get; }

    internal LinkedList<Type> Interfaces { get; }

    internal bool UseDefaultConventions { get; private set; }

    public ScanOptions FromAssembly(Assembly assembly)
    {
      if (assembly is null)
        throw new ArgumentNullException(nameof(assembly));

      Assemblies.AddLast(assembly);

      return this;
    }

    public ScanOptions FromAssembliesOf(IEnumerable<Type> types)
    {
      if (types is null)
        throw new ArgumentNullException(nameof(types));

      foreach (var type in types)
        Assemblies.AddLast(type.Assembly);

      return this;
    }

    public ScanOptions FromAssemblies(IEnumerable<Assembly> assemblies)
    {
      if (assemblies is null)
        throw new ArgumentNullException(nameof(assemblies));

      foreach (var assembly in assemblies)
        Assemblies.AddLast(assembly);

      return this;
    }

    public ScanOptions FromAssemblyBy<T>()
    {
      Assemblies.AddLast(typeof(T).Assembly);

      return this;
    }

    public ScanOptions AddTypesWithDefaultConventions()
    {
      UseDefaultConventions = true;

      return this;
    }

    public ScanOptions AddAllTypesOf<TInterface>()
    {
      var interfaceType = typeof(TInterface);
      if (!interfaceType.IsInterface)
        throw new ArgumentException($"Generic parameter '{interfaceType}' is not an interface.", nameof(TInterface));
      if (interfaceType.IsGenericType)
        throw new NotImplementedException("Scanning of generic interfaces is not implemented yet.");

      Interfaces.AddLast(interfaceType);

      return this;
    }
  }
}
