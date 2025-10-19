using ServiceXpert.Domain.Entities;
using System.Linq.Expressions;

namespace ServiceXpert.Domain.Helpers.Persistence.Includes;
public class IncludeExpressions<T> : List<Expression<Func<T, object>>> where T : EntityBase
{
    public IncludeExpressions() { }

    public IncludeExpressions(params Expression<Func<T, object>>[] includes)
    {
        AddRange(includes);
    }
}
