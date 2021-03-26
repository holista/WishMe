using System;

namespace WishMe.Service.Attributes
{
  [AttributeUsage(AttributeTargets.Property)]
  public class CompositeKeyAttribute: Attribute
  {
    public int Order { get; set; }
  }
}
