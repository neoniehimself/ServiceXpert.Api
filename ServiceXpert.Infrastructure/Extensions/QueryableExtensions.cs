using Microsoft.EntityFrameworkCore;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Helpers.Persistence.Includes;

namespace ServiceXpert.Infrastructure.Extensions;

internal static class QueryableExtensions
{
    internal static IQueryable<T> ApplyIncludeOption<T>(this IQueryable<T> query, IncludeOption<T> includeOption) where T : class, IEntityBase
    {
        if (includeOption.Includes is { Count: > 0 })
        {
            foreach (var include in includeOption.Includes)
            {
                query = query.Include(include);
            }
        }

        return query;
    }
}
