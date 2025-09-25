using FluentBuilder.Core;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Application.Shared;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Shared.ValueObjects;

namespace ServiceXpert.Application.Services.Contracts;
public interface IServiceBase<TId, TEntity, TDataObject> where TEntity : EntityBase<TId> where TDataObject : DataObjectBase<TId>
{
    Task<Result<TId>> CreateAsync<TDataObjectForCreate>(TDataObjectForCreate dataObjectForCreate) where TDataObjectForCreate : DataObjectBaseForCreate;

    Task<Result> DeleteByIdAsync(TId id);

    Task<Result<IEnumerable<TDataObject>>> GetAllAsync(IncludeOptions<TEntity>? includeOptions = null);

    Task<Result<TDataObject>> GetByIdAsync(TId id, IncludeOptions<TEntity>? includeOptions = null);

    Task<Result<PagedResult<TDataObject>>> GetPagedAllAsync(int pageNumber, int pageSize, IncludeOptions<TEntity>? includeOptions = null);

    Task<Result> IsExistsByIdAsync(TId id);

    Task<Result> UpdateByIdAsync<TDataObjectForUpdate>(TId id, TDataObjectForUpdate dataObjectForUpdate) where TDataObjectForUpdate : DataObjectBaseForUpdate;
}
