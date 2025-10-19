namespace ServiceXpert.Domain.ValueObjects.Pagination;
public class PaginationResult<T>
{
    public ICollection<T> Items { get; }

    public Pagination Pagination { get; }

    public PaginationResult()
    {
        this.Items = [];
        this.Pagination = new();
    }

    public PaginationResult(ICollection<T> items, Pagination pagination)
    {
        this.Items = items;
        this.Pagination = pagination;
    }
}
