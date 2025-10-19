using FluentBuilder.Core;
using FluentBuilder.Persistence;
using Microsoft.EntityFrameworkCore;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Repositories;
using ServiceXpert.Domain.Shared.ValueObjects;
using ServiceXpert.Domain.ValueObjects;
using ServiceXpert.Infrastructure.DbContexts;
using System.Linq.Expressions;

namespace ServiceXpert.Infrastructure.Repositories;
public abstract class RepositoryBase<TId, TEntity> : IRepositoryBase<TId, TEntity> where TEntity : EntityBase<TId>
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

    public async Task CreateAsync(TEntity entity)
    {
        await this.dbContext.Set<TEntity>().AddAsync(entity);
    }

    public async Task DeleteByIdAsync(TId id)
    {
        await this.dbContext.Set<TEntity>().Where(e => e.Id!.Equals(id)).ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, IncludeOptions<TEntity>? includeOptions = null)
    {
        IQueryable<TEntity> query = QueryBuilder.Build(this.dbContext.Set<TEntity>(), includeOptions);

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
    }

    public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, IncludeOptions<TEntity>? includeOptions = null)
    {
        IQueryable<TEntity> query = QueryBuilder.Build(this.dbContext.Set<TEntity>(), includeOptions);
        return query.Where(filter).SingleOrDefaultAsync();
    }

    public async Task<TEntity?> GetByIdAsync(TId id, IncludeOptions<TEntity>? includeOptions = null)
    {
        IQueryable<TEntity> query = QueryBuilder.Build(this.dbContext.Set<TEntity>(), includeOptions);
        return await query.SingleOrDefaultAsync(e => e.Id!.Equals(id));
    }

    public async Task<PagedResult<TEntity>> GetPagedAllAsync(int pageNumber, int pageSize,
        Expression<Func<TEntity, bool>>? filter = null, IncludeOptions<TEntity>? includeOptions = null)
    {
        IQueryable<TEntity> selectQuery = QueryBuilder.Build(this.dbContext.Set<TEntity>(), includeOptions);
        IQueryable<TEntity> totalCountQuery = this.dbContext.Set<TEntity>();

        if (filter != null)
        {
            selectQuery = selectQuery.Where(filter);
            totalCountQuery = totalCountQuery.Where(filter);
        }

        if (IsNumericType())
        {
            selectQuery = selectQuery.OrderBy(e => e.Id);
        }

        var entities = await selectQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<TEntity>(entities, new Pagination(await totalCountQuery.CountAsync(), pageSize, pageNumber));
    }

    public async Task<bool> IsExistsByIdAsync(TId id)
    {
        return await this.dbContext.Set<TEntity>().AnyAsync(e => e.Id!.Equals(id));
    }

    public async Task<int> SaveChangesAsync()
    {
        return await this.dbContext.SaveChangesAsync();
    }

    private static bool IsNumericType()
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
