using System;
using Microsoft.Extensions.DependencyInjection;

namespace WishMe.Service.Attributes
{
  [AttributeUsage(AttributeTargets.Class)]
  public sealed class LifetimeAttribute: Attribute
  {
    public ServiceLifetime Lifetime { get; }

    public LifetimeAttribute(ServiceLifetime lifetime)
    {
      Lifetime = lifetime;
    }
  }
}
