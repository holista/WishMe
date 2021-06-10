using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver.Linq;
using WishMe.Service.Dtos;

namespace WishMe.Service.Repositories
{
  public static class QueryableExtensions
  {
    private static IOrderedMongoQueryable<T> OrderBy<T>(this IMongoQueryable<T> source, string property)
    {
      return ApplyOrder(source, property, nameof(Queryable.OrderBy));
    }

    private static IOrderedMongoQueryable<T> OrderByDescending<T>(this IMongoQueryable<T> source, string property)
    {
      return ApplyOrder(source, property, nameof(Queryable.OrderByDescending));
    }

    private static IOrderedMongoQueryable<T> ThenBy<T>(this IOrderedMongoQueryable<T> source, string property)
    {
      return ApplyOrder(source, property, nameof(Queryable.ThenBy));
    }

    private static IOrderedMongoQueryable<T> ThenByDescending<T>(this IOrderedMongoQueryable<T> source, string property)
    {
      return ApplyOrder(source, property, nameof(Queryable.ThenByDescending));
    }

    private static IOrderedMongoQueryable<T> ApplyOrder<T>(IMongoQueryable<T> source, string property, string methodName)
    {
      string[] props = property.Split('.');
      var type = typeof(T);

      var arg = Expression.Parameter(type, "x");
      Expression expr = arg;

      foreach (string prop in props)
      {
        var propertyInfo = type.GetProperty(prop);
        Debug.Assert(propertyInfo != null);

        expr = Expression.Property(expr, propertyInfo);
        type = propertyInfo.PropertyType;
      }

      var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
      var lambda = Expression.Lambda(delegateType, expr, arg);

      var result = typeof(Queryable).GetMethods().Single(
          method => method.Name == methodName
            && method.IsGenericMethodDefinition
            && method.GetGenericArguments().Length == 2
            && method.GetParameters().Length == 2)
        .MakeGenericMethod(typeof(T), type)
        .Invoke(null, new object[] { source, lambda });

      if (result is null)
        throw new InvalidOperationException("Could not sort entities in given order.");

      return (IOrderedMongoQueryable<T>)result;
    }

    public static IMongoQueryable<T> OrderBySortingKeys<T>(this IMongoQueryable<T> source, SortingKeyDto[] sortingKeys)
    {
      var currentSortingKey = sortingKeys[0];

      var sorted = currentSortingKey.Descending
        ? source.OrderByDescending(currentSortingKey.Property)
        : source.OrderBy(currentSortingKey.Property);

      for (var i = 1; i < sortingKeys.Length; i++)
      {
        currentSortingKey = sortingKeys[i];

        sorted = currentSortingKey.Descending
          ? sorted.ThenByDescending(currentSortingKey.Property)
          : sorted.ThenBy(currentSortingKey.Property);
      }

      return sorted;
    }
  }
}
