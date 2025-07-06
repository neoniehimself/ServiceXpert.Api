using FluentBuilder.Core;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.ValueObjects;
using System.Linq.Expressions;

namespace ServiceXpert.Application.Services.Contracts;
public interface IServiceBase<TId, TEntity, TDataObject>
    where TEntity : EntityBase
    where TDataObject : DataObjectBase
{
    Task<TId> CreateAsync<TDataObjectForCreate>(TDataObjectForCreate dataObject)
        where TDataObjectForCreate : DataObjectBase;

    Task DeleteByIdAsync(TId id);

    Task<IEnumerable<TDataObject>> GetAllAsync(Expression<Func<TEntity, bool>>? condition = null,
        IncludeOptions<TEntity>? includeOptions = null);

    Task<TDataObject?> GetAsync(Expression<Func<TEntity, bool>> condition, IncludeOptions<TEntity>? includeOptions = null);

    Task<TDataObject?> GetByIdAsync(TId entityId, IncludeOptions<TEntity>? includeOptions = null);

    Task<PagedResult<TDataObject>> GetPagedAllAsync(int pageNumber = 1, int pageSize = 10,
        Expression<Func<TEntity, bool>>? condition = null, IncludeOptions<TEntity>? includeOptions = null);

    Task<bool> IsExistsByIdAsync(TId id);

    Task UpdateByIdAsync<TDataObjectForUpdate>(TId id, TDataObjectForUpdate dataObject)
        where TDataObjectForUpdate : DataObjectBase;
}