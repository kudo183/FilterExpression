using System.Linq;

namespace ExpressionBuilder
{
    public class Paging
    {
        public static int PageCount(int pageSize, int itemCount)
        {
            return (itemCount + pageSize - 1) / pageSize;
        }

        public static IQueryable<TSource> AddPaging<TSource>(
            IQueryable<TSource> query, int pageIndex, int pageSize, out int pageCount)
        {
            int itemCount = query.Count();

            query = AddPaging(query, pageIndex, itemCount, pageSize, out pageCount);

            return query;
        }

        public static IQueryable<TSource> AddPaging<TSource>(
            IQueryable<TSource> query, int pageIndex, int itemCount, int pageSize, out int pageCount)
        {
            pageCount = 0;

            if (pageIndex > 0)
            {
                pageCount = PageCount(pageSize, itemCount);
                var skippedItem = pageSize * (pageIndex - 1);
                var takeItem = itemCount - skippedItem;
                if (takeItem > pageSize)
                {
                    takeItem = pageSize;
                }
                query = query.Skip(skippedItem).Take(takeItem);
            }

            return query;
        }


        public static IQueryable<TSource> AddPaging<TSource>(
            IQueryable<TSource> query, int pageIndex, int pageSize, int pageCount)
        {
            int itemCount = query.Count();

            query = AddPaging(query, pageIndex, itemCount, pageSize, pageCount);

            return query;
        }

        public static IQueryable<TSource> AddPaging<TSource>(
            IQueryable<TSource> query, int pageIndex, int itemCount, int pageSize, int pageCount)
        {
            if (pageIndex > 0)
            {
                var skippedItem = pageSize * (pageIndex - 1);
                var takeItem = itemCount - skippedItem;
                if (takeItem > pageSize)
                {
                    takeItem = pageSize;
                }
                query = query.Skip(skippedItem).Take(takeItem);
            }

            return query;
        }
    }
}
