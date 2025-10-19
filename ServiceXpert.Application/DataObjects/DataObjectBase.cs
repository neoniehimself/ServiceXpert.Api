namespace ServiceXpert.Application.DataObjects;
public abstract class DataObjectBase<TId>
{
    public TId Id { get; set; } = default!;
}
