using FluentBuilder.Core;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.ValueObjects;
using System.Linq.Expressions;

namespace ServiceXpert.Domain.Repositories.Contracts;
public interface IRepositoryBase<TEntityId, TEntity> where TEntity : EntityBase
{
    Task<int> SaveChangesAsync();

    void Attach(TEntity entity);

    Task<TEntity?> GetByIdAsync(TEntityId entityId, IncludeOptions<TEntity>? includeOptions = null);

    Task<(IEnumerable<TEntity>, Pagination)> GetPagedAllAsync(int pageNumber, int pageSize,
        Expression<Func<TEntity, bool>>? condition = null, IncludeOptions<TEntity>? includeOptions = null);

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? condition = null,
        IncludeOptions<TEntity>? includeOptions = null);

    Task CreateAsync(TEntity entity);

    Task DeleteByIdAsync(TEntityId entityId);

    Task<bool> IsExistsByIdAsync(TEntityId entityId);
}
