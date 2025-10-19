using Microsoft.EntityFrameworkCore;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Helpers.Persistence.Includes;

namespace ServiceXpert.Infrastructure.Extensions;
internal static class QueryableExtensions
{
    internal static IQueryable<T> ApplyIncludeOptions<T>(this IQueryable<T> query, IncludeOptions<T>? includeOptions = null) where T : EntityBase
    {
        if (includeOptions?.Includes is { Count: > 0 })
        {
            foreach (var include in includeOptions.Includes)
            {
                query = query.Include(include);
            }
        }

        return query;
    }
}
