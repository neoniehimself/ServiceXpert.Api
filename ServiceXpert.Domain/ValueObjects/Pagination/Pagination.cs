namespace ServiceXpert.Domain.ValueObjects.Pagination;
public class Pagination
{
    public int TotalCount { get; }

    public int TotalPageCount { get; }

    public int PageSize { get; }

    public int CurrentPage { get; }

    public Pagination()
    {
    }

    public Pagination(int totalCount, int pageSize, int currentPage)
    {
        this.TotalCount = totalCount;
        this.PageSize = totalCount > 0 ? pageSize : 0;
        this.CurrentPage = totalCount > 0 ? currentPage : 0;
        this.TotalPageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
    }
}
