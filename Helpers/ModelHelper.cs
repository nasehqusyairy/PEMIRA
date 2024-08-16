using System.Linq.Expressions;
using System.Reflection;
using PEMIRA.Models;
namespace PEMIRA.Helpers
{
  public static class ModelHelper
  {
    public static TDestination MapProperties<TSource, TDestination>(TSource source)
        where TDestination : new()
    {
      TDestination destination = new();

      var sourceProperties = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
      var destinationProperties = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

      foreach (var sourceProperty in sourceProperties)
      {
        var destinationProperty = destinationProperties.FirstOrDefault(prop => prop.Name == sourceProperty.Name && prop.PropertyType == sourceProperty.PropertyType);
        if (destinationProperty != null && destinationProperty.CanWrite)
        {
          var value = sourceProperty.GetValue(source);
          destinationProperty.SetValue(destination, value);
        }
      }

      return destination;
    }

    public static void UpdateProperties<TSource, TTarget>(TSource source, TTarget target)
    {
      var sourceProperties = typeof(TSource).GetProperties();
      var targetProperties = typeof(TTarget).GetProperties();

      foreach (var sourceProperty in sourceProperties)
      {
        var targetProperty = targetProperties.FirstOrDefault(p => p.Name == sourceProperty.Name && p.PropertyType == sourceProperty.PropertyType);

        if (targetProperty != null)
        {
          var sourceValue = sourceProperty.GetValue(source);

          if (sourceValue is string strValue)
          {
            // Update hanya jika string tidak kosong atau whitespace
            if (!string.IsNullOrWhiteSpace(strValue))
            {
              targetProperty.SetValue(target, sourceValue);
            }
          }
          else if (sourceValue != null)
          {
            // Update jika nilai tidak null (untuk tipe non-string)
            targetProperty.SetValue(target, sourceValue);
          }
        }
      }
    }

    public static bool IsPropertyExist<T>(string propertyName)
    {
      return typeof(T).GetProperty(propertyName) != null;
    }

    private static Expression<Func<T, bool>>? BuildSearchExpression<T>(string propertyName, string search) where T : class
    {
      var parameter = Expression.Parameter(typeof(T), "entity");
      var property = Expression.Property(parameter, propertyName);
      var containsMethod = typeof(string).GetMethod("Contains", [typeof(string)]);

      if (containsMethod == null)
      {
        return null;
      }

      var condition = Expression.Call(property, containsMethod, Expression.Constant(search));
      return Expression.Lambda<Func<T, bool>>(condition, parameter);
    }

    public static List<T> GetEntities<T>(DatabaseContext _context, string defaultOrderProperty, string orderBy, string search = "", int page = 1, bool isAsc = true, int limit = 10) where T : class
    {
      if (!IsPropertyExist<T>(orderBy))
      {
        orderBy = defaultOrderProperty;
      }

      page = page < 1 ? 1 : page;

      var dbSet = _context.Set<T>();

      // Gunakan BuildSearchExpression untuk membuat ekspresi pencarian
      var searchExpression = BuildSearchExpression<T>(defaultOrderProperty, search);

      IQueryable<T> query = searchExpression != null ? dbSet.Where(searchExpression) : dbSet;

      // Sorting
      var parameter = Expression.Parameter(typeof(T), "entity");
      var orderByProperty = Expression.Property(parameter, orderBy);
      var orderByLambda = Expression.Lambda<Func<T, object>>(Expression.Convert(orderByProperty, typeof(object)), parameter);

      query = isAsc ? query.OrderBy(orderByLambda) : query.OrderByDescending(orderByLambda);

      // Pagination
      query = query.Skip((page - 1) * limit).Take(limit);

      return query.ToList();
    }

    public static int GetPageCount<T>(DatabaseContext _context, string search, int limit) where T : class
    {
      var dbSet = _context.Set<T>();

      // Gunakan BuildSearchExpression untuk membuat ekspresi pencarian
      var searchExpression = BuildSearchExpression<T>("Name", search);

      var count = searchExpression != null ? dbSet.Count(searchExpression) : dbSet.Count();

      return (int)Math.Ceiling(count / (double)limit);
    }


  }
}