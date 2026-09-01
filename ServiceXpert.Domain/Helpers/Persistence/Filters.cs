using System.Linq.Expressions;

namespace ServiceXpert.Domain.Helpers.Persistence;
public class Filters<TEntity>
{
    public Expression<Func<TEntity, bool>> Criteria { get; set; }

    public Filters(Expression<Func<TEntity, bool>> criteria)
    {
        this.Criteria = criteria;
    }
}
