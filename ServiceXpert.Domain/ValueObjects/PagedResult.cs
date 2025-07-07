namespace ServiceXpert.Domain.ValueObjects;
public class PagedResult<T>
{
    public IEnumerable<T> Items { get; }

    public Pagination Pagination { get; }

    public PagedResult()
    {
        this.Items = [];
        this.Pagination = new();
    }

    public PagedResult(IEnumerable<T> items, Pagination pagination)
    {
        this.Items = items;
        this.Pagination = pagination;
    }
}
