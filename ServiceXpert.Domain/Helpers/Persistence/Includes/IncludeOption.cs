using ServiceXpert.Domain.Entities;
using System.Linq.Expressions;

namespace ServiceXpert.Domain.Helpers.Persistence.Includes;

public class IncludeOption<T> where T : class, IEntityBase
{
    public IncludeExpressions<T> Includes { get; }

    public IncludeOption(IncludeExpressions<T> includes)
    {
        this.Includes = includes ?? [];
    }

    public IncludeOption(params Expression<Func<T, object>>[] includes)
    {
        this.Includes = [.. includes];
    }

    public void AddRange(IncludeExpressions<T> includes)
    {
        this.Includes.AddRange(includes);
    }

    public void AddRange(params Expression<Func<T, object>>[] includes)
    {
        this.Includes.AddRange(includes);
    }
}
