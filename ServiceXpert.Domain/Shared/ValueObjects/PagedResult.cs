namespace ServiceXpert.Domain.Shared.ValueObjects;
public class PagedResult<T>
{
    public ICollection<T> Items { get; }

    public Pagination Pagination { get; }

    public PagedResult()
    {
        this.Items = [];
        this.Pagination = new();
    }

    public PagedResult(ICollection<T> items, Pagination pagination)
    {
        this.Items = items;
        this.Pagination = pagination;
    }
}
