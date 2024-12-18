namespace SchoolProject.Core.Wrappers
{
    public class PaginatedResult<T>
    {
        public PaginatedResult(List<T> data)
        {
            Data = data;
        }



        internal PaginatedResult(bool succeeded, List<T> data = default, List<string> messages = null, int count = 0, int page = 1, int pageSize = 5)
        {
            Data = data;
            CurrentPage = page;
            Succeeded = succeeded;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }

        public static PaginatedResult<T> Success(List<T> data, int count, int page, int pageSize)
        {
            return new(true, data, null, count, page, pageSize);
        }

        public List<T> Data { get; private set; }
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public object Meta { get; set; }
        public int PageSize { get; private set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public List<string> Messages { get; set; } = new();
        public bool Succeeded { get; private set; }
    }
}
