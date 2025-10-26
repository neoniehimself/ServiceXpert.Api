using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Helpers.Persistence.Includes;
using ServiceXpert.Domain.ValueObjects.Pagination;
using System.Linq.Expressions;

namespace ServiceXpert.Domain.Repositories;
/// <summary>
/// Defines a generic repository interface that provides common data access operations.
/// </summary>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IRepositoryBase<TId, TEntity> where TEntity : EntityBase<TId>
{
    /// <summary>
    /// Attaches an entity to the current data context for tracking without performing any insert or update.
    /// </summary>
    /// <param name="entity">The entity to attach.</param>
    void Attach(TEntity entity);

    /// <summary>
    /// Asynchronously creates a new entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to create.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity to delete.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteByIdAsync(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves all entities that match the specified filters and include options.
    /// </summary>
    /// <param name="filters">An optional filter expression to apply to the query.</param>
    /// <param name="includeOptions">Optional include options for related entities.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the collection of entities.</returns>
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filters = null, IncludeOptions<TEntity>? includeOptions = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves a single entity that matches the specified filters and include options.
    /// </summary>
    /// <param name="filters">The filter expression to apply to the query.</param>
    /// <param name="includeOptions">Optional include options for related entities.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, <c>null</c>.</returns>
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filters, IncludeOptions<TEntity>? includeOptions = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves an entity by its identifier, with optional related data inclusion.
    /// </summary>
    /// <param name="id">The identifier of the entity to retrieve.</param>
    /// <param name="includeOptions">Optional include options for related entities.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, <c>null</c>.</returns>
    Task<TEntity?> GetByIdAsync(TId id, IncludeOptions<TEntity>? includeOptions = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves a paginated list of entities with optional filters and include options.
    /// </summary>
    /// <param name="pageNumber">The number of the page to retrieve (1-based).</param>
    /// <param name="pageSize">The size of each page.</param>
    /// <param name="filters">An optional filter expression to apply to the query.</param>
    /// <param name="includeOptions">Optional include options for related entities.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="PaginationResult{TEntity}"/> with the paged data.</returns>
    Task<PaginationResult<TEntity>> GetPagedAllAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>>? filters = null, IncludeOptions<TEntity>? includeOptions = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously checks whether an entity exists with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity to check.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <c>true</c> if the entity exists; otherwise, <c>false</c>.</returns>
    Task<bool> IsExistsByIdAsync(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously saves all changes made in the current data context to the underlying database.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
