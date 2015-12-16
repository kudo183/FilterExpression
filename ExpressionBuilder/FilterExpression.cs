using System.Collections.Generic;
using System.Linq;

namespace ExpressionBuilder
{
    public class FilterExpression
    {
        public int PageIndex { get; set; }
        public List<OrderByExpression.OrderOption> OrderOptions { get; set; }
        public List<WhereExpression.WhereOption> WhereOptions { get; set; }

        public static IQueryable<TSource> AddFilterExpression<TSource>
            (IQueryable<TSource> source, FilterExpression filter, int pageSize, out int pageCount)
        {
            source = WhereExpression.AddWhereExpression(source, filter.WhereOptions);
            source = OrderByExpression.AddOrderByExpression(source, filter.OrderOptions);
            source = Paging.AddPaging(source, filter.PageIndex, pageSize, out pageCount);
            return source;
        }

        public static IQueryable<TSource> AddFilterExpression<TSource>
            (IQueryable<TSource> source, string filterJson, int pageSize, out int pageCount)
        {
            var filter = FromJsonString(filterJson);
            source = AddFilterExpression(source, filter, pageSize, out pageCount);
            return source;
        }

        public static FilterExpression FromJsonString(string json)
        {
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<FilterExpression>(json);
            return result;
        }

        public static string ToJsonString(FilterExpression filterExpression)
        {
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(filterExpression);
            return result;
        }
    }
}
