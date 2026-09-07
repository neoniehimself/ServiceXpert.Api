using Mapster;
using MapsterMapper;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Application.Enums;
using ServiceXpert.Application.Models;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Helpers.Persistence.Includes;
using ServiceXpert.Domain.Repositories;

namespace ServiceXpert.Application.Services.Concretes;

internal abstract class ServiceBase<TId, TEntity, TDataObject> : IServiceBase<TId, TEntity, TDataObject>
    where TEntity : EntityBase<TId>
    where TDataObject : DataObjectBase<TId>
{
    private readonly IMapper mapper;
    private readonly IRepositoryBase<TId, TEntity> repositoryBase;

    public ServiceBase(IMapper mapper, IRepositoryBase<TId, TEntity> repositoryBase)
    {
        this.mapper = mapper;
        this.repositoryBase = repositoryBase;
    }

    public virtual async Task<ServiceResult<TId>> CreateAsync<TCreateDataObject>(TCreateDataObject createDataObject, CancellationToken cancellationToken = default) where TCreateDataObject : CreateDataObjectBase
    {
        TEntity entity = createDataObject.Adapt<TEntity>();

        await this.repositoryBase.CreateAsync(entity, cancellationToken);
        await this.repositoryBase.SaveChangesAsync(cancellationToken);

        return ServiceResult<TId>.Ok(entity.Id);
    }

    public virtual async Task<ServiceResult> DeleteByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        await this.repositoryBase.DeleteByIdAsync(id, cancellationToken);
        await this.repositoryBase.SaveChangesAsync(cancellationToken);
        return ServiceResult.Ok();
    }

    public virtual async Task<ServiceResult<TDataObject>> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await this.repositoryBase.GetByIdAsync(id, cancellationToken);
        return entity is not null
            ? ServiceResult<TDataObject>.Ok(entity.Adapt<TDataObject>())
            : ServiceResult<TDataObject>.Fail(ServiceResultStatus.NotFound, [$"{typeof(TEntity).Name} not found. Id: {id}"]);
    }

    public virtual async Task<ServiceResult<TDataObject>> GetByIdAsync(TId id, IncludeOption<TEntity> includeOption, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await this.repositoryBase.GetByIdAsync(id, includeOption, cancellationToken);
        return entity is not null
            ? ServiceResult<TDataObject>.Ok(entity.Adapt<TDataObject>())
            : ServiceResult<TDataObject>.Fail(ServiceResultStatus.NotFound, [$"{typeof(TEntity).Name} not found. Id: {id}"]);
    }

    public virtual async Task<ServiceResult> IsExistsByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await this.repositoryBase.IsExistsByIdAsync(id, cancellationToken)
            ? ServiceResult.Ok()
            : ServiceResult.Fail(ServiceResultStatus.NotFound, [$"{typeof(TEntity).Name} not found. Id: {id}"]);
    }

    public virtual async Task<ServiceResult> UpdateByIdAsync<TUpdateDataObject>(TId id, TUpdateDataObject updateDataObject, CancellationToken cancellationToken = default) where TUpdateDataObject : UpdateDataObjectBase
    {
        TEntity? entityToUpdate = await this.repositoryBase.GetByIdAsync(id, cancellationToken);
        if (entityToUpdate is not null)
        {
            /* Attach the entity to the change tracker before updating or mapping data
             * to ensure that only modified values are persisted. */
            this.repositoryBase.Attach(entityToUpdate);
            this.mapper.Map(updateDataObject, entityToUpdate);

            await this.repositoryBase.SaveChangesAsync(cancellationToken);
            return ServiceResult.Ok();
        }

        return ServiceResult.Fail(ServiceResultStatus.NotFound, [$"{typeof(TEntity).Name} not found. Id: {id}"]);
    }
}
