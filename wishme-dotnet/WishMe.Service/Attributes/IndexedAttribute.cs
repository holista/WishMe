using System;

namespace WishMe.Service.Attributes
{
  [AttributeUsage(AttributeTargets.Property)]
  public class IndexedAttribute: Attribute
  {
    public bool IsUnique { get; set; } = true;
  }
}
