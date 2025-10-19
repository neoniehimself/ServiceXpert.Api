using ServiceXpert.Domain.Entities;
using System.Linq.Expressions;

namespace ServiceXpert.Domain.Helpers.Persistence.Includes;
public class IncludeOptions<T> where T : EntityBase
{
    public IncludeExpressions<T> Includes { get; }

    public IncludeOptions(IncludeExpressions<T> includes)
    {
        this.Includes = includes ?? [];
    }

    public IncludeOptions(params Expression<Func<T, object>>[] includes)
    {
        this.Includes = [.. includes];
    }

    public void AddRange(IncludeExpressions<T> expressions)
    {
        this.Includes.AddRange(expressions);
    }

    public void AddRange(params Expression<Func<T, object>>[] expressions)
    {
        this.Includes.AddRange(expressions);
    }
}
