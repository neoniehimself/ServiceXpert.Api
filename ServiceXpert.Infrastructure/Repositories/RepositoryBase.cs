using FluentBuilder.Core;
using FluentBuilder.Persistence;
using Microsoft.EntityFrameworkCore;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Repositories.Contracts;
using ServiceXpert.Domain.ValueObjects;
using ServiceXpert.Infrastructure.DbContexts;
using System.Linq.Expressions;

namespace ServiceXpert.Infrastructure.Repositories;
public abstract class RepositoryBase<TEntityId, TEntity> : IRepositoryBase<TEntityId, TEntity> where TEntity : EntityBase
{
    private readonly SxpDbContext dbContext;

    protected string EntityId { get => string.Concat(typeof(TEntity).Name, "Id"); }

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

    public async Task DeleteByIdAsync(TEntityId entityId)
    {
        await this.dbContext.Set<TEntity>()
            .Where(e => EF.Property<TEntityId>(e, this.EntityId)!.Equals(entityId)).ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        IncludeOptions<TEntity>? includeOptions = null)
    {
        IQueryable<TEntity> query = QueryBuilder.Build(this.dbContext.Set<TEntity>(), includeOptions);

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
    }

    public Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> filter,
        IncludeOptions<TEntity>? includeOptions = null)
    {
        IQueryable<TEntity> query = QueryBuilder.Build(this.dbContext.Set<TEntity>(), includeOptions);
        return query.Where(filter).SingleOrDefaultAsync();
    }

    public async Task<TEntity?> GetByIdAsync(TEntityId entityId, IncludeOptions<TEntity>? includeOptions = null)
    {
        IQueryable<TEntity> query = QueryBuilder.Build(this.dbContext.Set<TEntity>(), includeOptions);
        return await query.SingleOrDefaultAsync(e => EF.Property<TEntityId>(e, this.EntityId)!.Equals(entityId));
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

        var entities = await selectQuery
            .OrderBy(e => EF.Property<TEntityId>(e, this.EntityId))
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<TEntity>(entities, new Pagination(await totalCountQuery.CountAsync(), pageSize, pageNumber));
    }

    public async Task<bool> IsExistsByIdAsync(TEntityId entityId)
    {
        return await this.dbContext.Set<TEntity>()
            .AnyAsync(e => EF.Property<TEntityId>(e, this.EntityId)!.Equals(entityId));
    }

    public async Task<int> SaveChangesAsync()
    {
        return await this.dbContext.SaveChangesAsync();
    }
}
