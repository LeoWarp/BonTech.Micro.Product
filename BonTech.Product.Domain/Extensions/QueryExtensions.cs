using System.Linq.Expressions;

namespace BonTech.Product.Domain.Extensions;

public static class QueryExtensions
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> predicate)
    {
        if (condition)
            return source.Where(predicate);
        return source;
    }
    
    public static IQueryable<T> OrderWithCondition<T>(this IQueryable<T> source, bool condition,  Expression<Func<T,object>> predicate)
    {
        if (condition)
            return source.OrderByDescending(predicate);
        return source;
    }
}