using System;
using System.Collections.Generic;
using System.Linq;

namespace DeVeeraApp.Utils
{
    public static class PagedResultExtensions
    {
        public static PagedResult<T> GetPaged<T>(this IEnumerable<T> query, int page, int pageSize, int TotalRecords)
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = TotalRecords
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            //var skip = (page - 1) * pageSize;
            result.Results = query.ToList();

            return result;
        }
      
        public static IList<T> GetListPageData<T>(this IList<T> result, int page, int pageSize)
        {
            var resultList = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = result.Count()
            };

            var pageCount = (double)resultList.RowCount / pageSize;
            resultList.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            resultList.Results = result.Skip(skip).Take(pageSize).ToList();

            return resultList.Results;
        }
    }
}