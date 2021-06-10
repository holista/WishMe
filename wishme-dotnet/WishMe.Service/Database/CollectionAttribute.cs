using System;

namespace WishMe.Service.Database
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CollectionAttribute : Attribute
    {
        public string Name { get; }

        public CollectionAttribute(string name)
        {
            Name = name;
        }

    }
}
