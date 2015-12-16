using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionBuilder
{
    public class OrderByExpression
    {
        public static IQueryable<TSource> AddOrderByExpression<TSource>
            (IQueryable<TSource> source, string propertyPath, bool isAscending = true)
        {
            var p = Expression.Parameter(typeof(TSource), "p");
            var parts = propertyPath.Split('.');

            Expression orderByProperty = parts.Aggregate<string, Expression>(p, Expression.Property);

            LambdaExpression lambda = Expression.Lambda(orderByProperty, new[] { p });

            string methodName = isAscending ? "OrderBy" : "OrderByDescending";
            MethodCallExpression orderByCallExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { source.ElementType, orderByProperty.Type },
                source.Expression,
                lambda);

            return source.Provider.CreateQuery<TSource>(orderByCallExpression);
        }

        public static IQueryable<TSource> AddOrderByExpression<TSource>
            (IQueryable<TSource> source, List<OrderOption> orderOptions)
        {
            if (orderOptions == null || orderOptions.Count == 0)
                return source;

            var p = Expression.Parameter(typeof(TSource), "p");
            var parts = orderOptions[0].PropertyPath.Split('.');

            var orderByProperty = parts.Aggregate<string, Expression>(p, Expression.Property);

            var lambda = Expression.Lambda(orderByProperty, new[] { p });

            string methodName = orderOptions[0].IsAscending ? "OrderBy" : "OrderByDescending";
            MethodCallExpression orderByCallExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { source.ElementType, orderByProperty.Type },
                source.Expression,
                lambda);

            for (int i = 1; i < orderOptions.Count; i++)
            {
                parts = orderOptions[i].PropertyPath.Split('.');
                orderByProperty = parts.Aggregate<string, Expression>(p, Expression.Property);
                lambda = Expression.Lambda(orderByProperty, new[] { p });

                methodName = orderOptions[i].IsAscending ? "ThenBy" : "ThenByDescending";
                orderByCallExpression = Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new[] { source.ElementType, orderByProperty.Type },
                    orderByCallExpression,
                    lambda);
            }
            return source.Provider.CreateQuery<TSource>(orderByCallExpression);
        }

        public class OrderOption
        {
            public string PropertyPath { get; set; }
            public bool IsAscending { get; set; }
        }
    }
}
