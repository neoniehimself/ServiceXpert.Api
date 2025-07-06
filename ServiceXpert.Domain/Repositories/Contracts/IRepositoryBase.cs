using FluentBuilder.Core;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.ValueObjects;
using System.Linq.Expressions;

namespace ServiceXpert.Domain.Repositories.Contracts;
public interface IRepositoryBase<TEntityId, TEntity> where TEntity : EntityBase
{
    void Attach(TEntity entity);

    Task CreateAsync(TEntity entity);

    Task DeleteByIdAsync(TEntityId entityId);

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? condition = null,
        IncludeOptions<TEntity>? includeOptions = null);

    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> condition, IncludeOptions<TEntity>? includeOptions = null);

    Task<TEntity?> GetByIdAsync(TEntityId entityId, IncludeOptions<TEntity>? includeOptions = null);

    Task<PagedResult<TEntity>> GetPagedAllAsync(int pageNumber, int pageSize,
        Expression<Func<TEntity, bool>>? condition = null, IncludeOptions<TEntity>? includeOptions = null);

    Task<bool> IsExistsByIdAsync(TEntityId entityId);

    Task<int> SaveChangesAsync();
}
