using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionBuilder
{
    public class WhereExpression
    {
        public static IQueryable<TSource> AddWhereExpression<TSource>
            (IQueryable<TSource> source, string propertyPath, string predicate, string value)
        {
            var p = Expression.Parameter(typeof(TSource), "p");
            var parts = propertyPath.Split('.');

            var whereProperty = parts.Aggregate<string, Expression>(p, Expression.Property);
            var typeConverter = TypeDescriptor.GetConverter(whereProperty.Type);
            var constant = Expression.Constant(typeConverter.ConvertFromString(value), whereProperty.Type);
            var predicateBody = WhereOption.GetPredicateExpression(predicate, whereProperty, constant);

            var lamda = Expression.Lambda<Func<TSource, bool>>(predicateBody, p);
            return source.Where(lamda);
        }

        public static IQueryable<TSource> AddWhereExpression<TSource>
            (IQueryable<TSource> source, List<WhereOption> whereOptions)
        {
            if (whereOptions == null || whereOptions.Count == 0)
                return source;

            foreach (var whereOption in whereOptions)
            {
                source = AddWhereExpression(source, whereOption.PropertyPath, whereOption.Predicate, whereOption.Value);
            }

            return source;
        }

        public class WhereOption
        {
            public string PropertyPath { get; set; }
            public string Predicate { get; set; }
            public string Value { get; set; }

            public const string GreaterThan = ">";
            public const string GreaterThanOrEqual = ">=";
            public const string LessThan = "<";
            public const string LessThanOrEqual = "<=";
            public const string Equal = "=";
            public const string NotEqual = "!=";
            public const string StartsWith = "~";
            public const string Contains = "*";
            public const string NotContains = "!*";

            public static Expression GetPredicateExpression(string predicate, Expression left, Expression right)
            {
                switch (predicate)
                {
                    case GreaterThan:
                        return Expression.GreaterThan(left, right);
                    case GreaterThanOrEqual:
                        return Expression.GreaterThanOrEqual(left, right);
                    case LessThan:
                        return Expression.LessThan(left, right);
                    case LessThanOrEqual:
                        return Expression.LessThanOrEqual(left, right);
                    case Equal:
                        return Expression.Equal(left, right);
                    case NotEqual:
                        return Expression.NotEqual(left, right);
                    case StartsWith:
                        return Expression.Call(left, "StartsWith", null, right);
                    case Contains:
                        return Expression.Call(left, "Contains", null, right);
                    case NotContains:
                        return Expression.Not(Expression.Call(left, "Contains", null, right));
                }

                return Expression.Equal(left, right);
            }
        }
    }
}
