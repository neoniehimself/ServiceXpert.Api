using Microsoft.EntityFrameworkCore;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Helpers.Persistence;
using ServiceXpert.Domain.Helpers.Persistence.Includes;
using ServiceXpert.Domain.Repositories;
using ServiceXpert.Domain.ValueObjects.Pagination;
using ServiceXpert.Infrastructure.DbContexts;
using ServiceXpert.Infrastructure.Extensions;

namespace ServiceXpert.Infrastructure.Repositories;

internal abstract class RepositoryBase<TId, TEntity> : IRepositoryBase<TId, TEntity> where TEntity : EntityBase<TId>
{
    private readonly SxpDbContext dbContext;

    public RepositoryBase(SxpDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Attach(TEntity entity)
    {
        this.dbContext.Set<TEntity>().Attach(entity);
    }

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await this.dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public async Task DeleteByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        await GetQueryBase(nameof(DeleteByIdAsync))
            .Where(e => e.Id!.Equals(id))
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await GetQueryBase(nameof(GetAllAsync)).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(FilterOption<TEntity> filterOption, CancellationToken cancellationToken = default)
    {
        return await GetQueryBase(nameof(GetAllAsync))
            .Where(filterOption.Filters)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(IncludeOption<TEntity> includeOption, CancellationToken cancellationToken = default)
    {
        return await GetQueryBase(nameof(GetAllAsync))
            .ApplyIncludeOption(includeOption)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(FilterOption<TEntity> filterOption, IncludeOption<TEntity> includeOption, CancellationToken cancellationToken = default)
    {
        return await GetQueryBase(nameof(GetAllAsync))
            .Where(filterOption.Filters)
            .ApplyIncludeOption(includeOption)
            .ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(FilterOption<TEntity> filterOption, CancellationToken cancellationToken = default)
    {
        return await GetQueryBase(nameof(GetAsync))
            .Where(filterOption.Filters)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(FilterOption<TEntity> filterOption, IncludeOption<TEntity> includeOption, CancellationToken cancellationToken = default)
    {
        return await GetQueryBase(nameof(GetAsync))
            .Where(filterOption.Filters)
            .ApplyIncludeOption(includeOption)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await GetQueryBase(nameof(GetByIdAsync)).SingleOrDefaultAsync(e => e.Id!.Equals(id), cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(TId id, IncludeOption<TEntity> includeOption, CancellationToken cancellationToken = default)
    {
        return await GetQueryBase(nameof(GetByIdAsync))
            .ApplyIncludeOption(includeOption)
            .SingleOrDefaultAsync(e => e.Id!.Equals(id), cancellationToken);
    }

    public async Task<PaginationResult<TEntity>> GetPagedAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> selectQuery = GetQueryBase($"{nameof(GetPagedAllAsync)}.{nameof(selectQuery)}");
        IQueryable<TEntity> totalCountQuery = GetQueryBase($"{nameof(GetPagedAllAsync)}.{nameof(totalCountQuery)}");

        if (IsNumericType())
        {
            selectQuery = selectQuery.OrderBy(e => e.Id);
        }

        List<TEntity> entities = await selectQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        Pagination pagination = new(await totalCountQuery.CountAsync(cancellationToken), pageSize, pageNumber);
        return new PaginationResult<TEntity>(entities, pagination);
    }

    public async Task<PaginationResult<TEntity>> GetPagedAllAsync(int pageNumber, int pageSize, FilterOption<TEntity> filterOption, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> selectQuery =
            GetQueryBase($"{nameof(GetPagedAllAsync)}.{nameof(selectQuery)}")
            .Where(filterOption.Filters);

        IQueryable<TEntity> totalCountQuery =
            GetQueryBase($"{nameof(GetPagedAllAsync)}.{nameof(totalCountQuery)}")
            .Where(filterOption.Filters);

        if (IsNumericType())
        {
            selectQuery = selectQuery.OrderBy(e => e.Id);
        }

        List<TEntity> entities = await selectQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        Pagination pagination = new(await totalCountQuery.CountAsync(cancellationToken), pageSize, pageNumber);
        return new PaginationResult<TEntity>(entities, pagination);
    }

    public async Task<PaginationResult<TEntity>> GetPagedAllAsync(int pageNumber, int pageSize, IncludeOption<TEntity> includeOption, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> selectQuery =
            GetQueryBase($"{nameof(GetPagedAllAsync)}.{nameof(selectQuery)}")
            .ApplyIncludeOption(includeOption);

        IQueryable<TEntity> totalCountQuery = GetQueryBase($"{nameof(GetPagedAllAsync)}.{nameof(totalCountQuery)}");

        if (IsNumericType())
        {
            selectQuery = selectQuery.OrderBy(e => e.Id);
        }

        List<TEntity> entities = await selectQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        Pagination pagination = new(await totalCountQuery.CountAsync(cancellationToken), pageSize, pageNumber);
        return new PaginationResult<TEntity>(entities, pagination);
    }

    public async Task<PaginationResult<TEntity>> GetPagedAllAsync(int pageNumber, int pageSize, FilterOption<TEntity> filterOption, IncludeOption<TEntity> includeOption, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> selectQuery =
            GetQueryBase($"{nameof(GetPagedAllAsync)}.{nameof(selectQuery)}")
            .Where(filterOption.Filters)
            .ApplyIncludeOption(includeOption);

        IQueryable<TEntity> totalCountQuery =
            GetQueryBase($"{nameof(GetPagedAllAsync)}.{nameof(totalCountQuery)}")
            .Where(filterOption.Filters);

        if (IsNumericType())
        {
            selectQuery = selectQuery.OrderBy(e => e.Id);
        }

        List<TEntity> entities = await selectQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        Pagination pagination = new(await totalCountQuery.CountAsync(cancellationToken), pageSize, pageNumber);
        return new PaginationResult<TEntity>(entities, pagination);
    }

    public Task<bool> IsExistsByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return GetQueryBase(nameof(IsExistsByIdAsync)).AnyAsync(e => e.Id!.Equals(id), cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await this.dbContext.SaveChangesAsync(cancellationToken);
    }

    private IQueryable<TEntity> GetQueryBase(string operationName)
    {
        return this.dbContext.Set<TEntity>().TagWith($"{nameof(RepositoryBase<,>)}.{operationName}");
    }

    protected static bool IsNumericType()
    {
        var type = typeof(TId);
        return type == typeof(byte) || type == typeof(sbyte) ||
               type == typeof(short) || type == typeof(ushort) ||
               type == typeof(int) || type == typeof(uint) ||
               type == typeof(long) || type == typeof(ulong) ||
               type == typeof(float) || type == typeof(double) ||
               type == typeof(decimal);
    }
}
