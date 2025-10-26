using ServiceXpert.Application.DataObjects;
using ServiceXpert.Application.Models;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Helpers.Persistence.Includes;
using ServiceXpert.Domain.ValueObjects.Pagination;

namespace ServiceXpert.Application.Services.Contracts;
public interface IServiceBase<TId, TEntity, TDataObject>
    where TEntity : EntityBase<TId>
    where TDataObject : DataObjectBase<TId>
{
    Task<ServiceResult<TId>> CreateAsync<TCreateDataObject>(TCreateDataObject createDataObject, CancellationToken cancellationToken = default) where TCreateDataObject : CreateDataObjectBase;

    Task<ServiceResult> DeleteByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task<ServiceResult<IEnumerable<TDataObject>>> GetAllAsync(IncludeOptions<TEntity>? includeOptions = null, CancellationToken cancellationToken = default);

    Task<ServiceResult<TDataObject>> GetByIdAsync(TId id, IncludeOptions<TEntity>? includeOptions = null, CancellationToken cancellationToken = default);

    Task<ServiceResult<PaginationResult<TDataObject>>> GetPagedAllAsync(int pageNumber, int pageSize, IncludeOptions<TEntity>? includeOptions = null, CancellationToken cancellationToken = default);

    Task<ServiceResult> IsExistsByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task<ServiceResult> UpdateByIdAsync<TUpdateDataObject>(TId id, TUpdateDataObject updateDataObject, CancellationToken cancellationToken = default) where TUpdateDataObject : UpdateDataObjectBase;
}
