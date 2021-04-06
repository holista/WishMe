using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WishMe.Service.Attributes;

namespace WishMe.Service.Entities
{
  public class DataContext: DbContext
  {
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      var types = Assembly
        .GetExecutingAssembly()
        .GetExportedTypes()
        .Where(x => x.IsSubclassOf(typeof(TimestampEntityBase)));

      foreach (var type in types)
      {
        if (type.IsAbstract)
          continue;

        modelBuilder.Entity(type);

        SetCompositeKeysIfNeed(modelBuilder, type);

        SetIndicesIfNeed(modelBuilder, type);
      }

      foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
        foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
    }

    private static void SetCompositeKeysIfNeed(ModelBuilder modelBuilder, Type type)
    {
      var compositeKeyNames = type.GetProperties()
          .Select(property => new
          {
            Property = property,
            Attribute = property.GetCustomAttribute<CompositeKeyAttribute>()
          })
          .Where(compositeKey => compositeKey.Attribute != null)
          .OrderBy(compositeKey => compositeKey.Attribute!.Order)
          .Select(compositeKey => compositeKey.Property.Name)
          .ToArray();

      if (compositeKeyNames.Any())
        modelBuilder.Entity(type).HasKey(compositeKeyNames);
    }

    private static void SetIndicesIfNeed(ModelBuilder modelBuilder, Type type)
    {
      foreach (var property in type.GetProperties())
      {
        var attribute = property.GetCustomAttribute<IndexedAttribute>();
        if (attribute is null)
          continue;

        modelBuilder.Entity(type)
            .HasIndex(property.Name)
            .IsUnique(attribute.IsUnique);
      }
    }
  }
}
