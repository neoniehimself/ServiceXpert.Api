using System.Linq.Expressions;

namespace ServiceXpert.Domain.Helpers.Persistence;

public class FilterOption<TEntity>
{
    public Expression<Func<TEntity, bool>> Filters { get; set; }

    public FilterOption(Expression<Func<TEntity, bool>> filters)
    {
        this.Filters = filters;
    }
}
