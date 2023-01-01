namespace InvoiceApp.Helpers
{
    public class PagedList<T> : List<T>, IPagedList
    {
        public uint CurrentPage { get; private set; }
        public uint PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public uint PageCount { get; private set; }

        public bool HasPrevious => CurrentPage != 0;
        public bool HasNext => CurrentPage != (PageCount - 1);

        public PagedList(IEnumerable<T> source, uint currentPage, uint pageSize, int totalCount)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            PageCount = (uint)Math.Ceiling(totalCount / (double)pageSize);

            AddRange(source);
        }
    }

    public interface IPagedList
    {
        public uint CurrentPage { get; }
        public uint PageSize { get; }
        public int TotalCount { get; }
        public uint PageCount { get; }
        public bool HasPrevious { get; }
        public bool HasNext { get; }
    }
}
