namespace Trading.Api.Helpers
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; private set; } = Enumerable.Empty<T>();
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalItems { get; private set; }
        public int TotalPages { get; private set; }

        private PagedResult() { }

        /// <summary>
        /// Factory method to create a paged result from a full collection
        /// </summary>
        public static PagedResult<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var totalItems = source.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = source.Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToList();

            return new PagedResult<T>
            {
                Items = items,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }
    }

}
