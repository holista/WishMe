using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using WishMe.Service.Dtos;

namespace WishMe.Service.Repositories
{
  public static class QueryableExtensions
  {
    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
    {
      return ApplyOrder(source, property, nameof(Queryable.OrderBy));
    }

    public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
    {
      return ApplyOrder(source, property, nameof(Queryable.OrderByDescending));
    }

    public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property)
    {
      return ApplyOrder(source, property, nameof(Queryable.ThenBy));
    }

    public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property)
    {
      return ApplyOrder(source, property, nameof(Queryable.ThenByDescending));
    }

    public static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
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

      return (IOrderedQueryable<T>)result;
    }

    public static IQueryable<T> OrderBySortingKeys<T>(this IQueryable<T> source, SortingKeyDto[] sortingKeys)
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
