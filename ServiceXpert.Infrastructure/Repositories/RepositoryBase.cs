using Microsoft.EntityFrameworkCore;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Helpers.Persistence.Includes;
using ServiceXpert.Domain.Repositories;
using ServiceXpert.Domain.ValueObjects.Pagination;
using ServiceXpert.Infrastructure.DbContexts;
using ServiceXpert.Infrastructure.Extensions;
using System.Linq.Expressions;

namespace ServiceXpert.Infrastructure.Repositories;
internal abstract class RepositoryBase<TId, TEntity> : IRepositoryBase<TId, TEntity> where TEntity : EntityBase<TId>
{
    private readonly SxpDbContext dbContext;

    public string ClassName { get => nameof(RepositoryBase<TId, TEntity>); }

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
        await this.dbContext.Set<TEntity>().TagWith($"{this.ClassName}.{nameof(DeleteByIdAsync)}").Where(e => e.Id!.Equals(id)).ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filters = null, IncludeOptions<TEntity>? includeOptions = null)
    {
        IQueryable<TEntity> query = this.dbContext.Set<TEntity>().TagWith($"{this.ClassName}.{nameof(GetAllAsync)}").ApplyIncludeOptions(includeOptions);

        if (filters != null)
        {
            query = query.Where(filters);
        }

        return await query.ToListAsync();
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filters, IncludeOptions<TEntity>? includeOptions = null)
    {
        return await this.dbContext.Set<TEntity>().TagWith($"{this.ClassName}.{nameof(GetAsync)}").ApplyIncludeOptions(includeOptions).Where(filters).SingleOrDefaultAsync();
    }

    public async Task<TEntity?> GetByIdAsync(TId id, IncludeOptions<TEntity>? includeOptions = null)
    {
        return await this.dbContext.Set<TEntity>().TagWith($"{this.ClassName}.{nameof(GetByIdAsync)}").ApplyIncludeOptions(includeOptions).SingleOrDefaultAsync(e => e.Id!.Equals(id));
    }

    public async Task<PaginationResult<TEntity>> GetPagedAllAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<TEntity, bool>>? filters = null,
        IncludeOptions<TEntity>? includeOptions = null)
    {
        IQueryable<TEntity> selectQuery = this.dbContext.Set<TEntity>().TagWith($"{this.ClassName}.{nameof(GetPagedAllAsync)}.{nameof(selectQuery)}").ApplyIncludeOptions(includeOptions);
        IQueryable<TEntity> totalCountQuery = this.dbContext.Set<TEntity>().TagWith($"{this.ClassName}.{nameof(GetPagedAllAsync)}.{nameof(totalCountQuery)}");

        if (filters != null)
        {
            selectQuery = selectQuery.Where(filters);
            totalCountQuery = totalCountQuery.Where(filters);
        }

        if (IsNumericType())
        {
            selectQuery = selectQuery.OrderBy(e => e.Id);
        }

        var entities = await selectQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return new PaginationResult<TEntity>(entities, new Pagination(await totalCountQuery.CountAsync(), pageSize, pageNumber));
    }

    public async Task<bool> IsExistsByIdAsync(TId id)
    {
        return await this.dbContext.Set<TEntity>().TagWith($"{this.ClassName}.{nameof(IsExistsByIdAsync)}").AnyAsync(e => e.Id!.Equals(id));
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
