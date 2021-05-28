using System.Collections.Generic;

namespace WebStore.Domain
{
    public class PageQueryResult<T>
    {
        public IEnumerable<T> Items { get; init; }

        public int TotalCount { get; init; }

        //public int Page { get; init; }
        //public int TotalPageCount => (int) Math.Ceiling((double) TotalCount / Items.Count());
    }
}