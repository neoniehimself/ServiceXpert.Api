using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Helpers.Persistence;
using ServiceXpert.Domain.Helpers.Persistence.Includes;
using ServiceXpert.Domain.ValueObjects.Pagination;

namespace ServiceXpert.Domain.Repositories;

public interface IRepositoryBase<TId, TEntity> where TEntity : EntityBase<TId>
{
    void Attach(TEntity entity);

    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync(FilterOption<TEntity> filterOption, IncludeOption<TEntity> includeOption, CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync(TId id, IncludeOption<TEntity> includeOption, CancellationToken cancellationToken = default);

    Task<PaginationResult<TEntity>> GetPagedAllAsync(int pageNumber, int pageSize, FilterOption<TEntity> filterOption, CancellationToken cancellationToken = default);

    Task<bool> IsExistsByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
