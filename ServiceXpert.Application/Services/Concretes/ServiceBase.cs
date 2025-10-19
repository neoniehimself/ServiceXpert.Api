using Mapster;
using MapsterMapper;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Application.Enums;
using ServiceXpert.Application.Models;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Helpers.Persistence.Includes;
using ServiceXpert.Domain.Repositories;
using ServiceXpert.Domain.ValueObjects.Pagination;

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

    public virtual async Task<ServiceResult<TId>> CreateAsync<TCreateDataObject>(TCreateDataObject createDataObject) where TCreateDataObject : CreateDataObjectBase
    {
        TEntity entity = createDataObject.Adapt<TEntity>();

        await this.repositoryBase.CreateAsync(entity);
        await this.repositoryBase.SaveChangesAsync();

        return ServiceResult<TId>.Ok(entity.Id);
    }

    public async Task<ServiceResult> DeleteByIdAsync(TId id)
    {
        await this.repositoryBase.DeleteByIdAsync(id);
        await this.repositoryBase.SaveChangesAsync();

        return ServiceResult.Ok();
    }

    public async Task<ServiceResult<IEnumerable<TDataObject>>> GetAllAsync(IncludeOptions<TEntity>? includeOptions = null)
    {
        IEnumerable<TEntity> entities = await this.repositoryBase.GetAllAsync(includeOptions: includeOptions);
        ICollection<TDataObject> dataObjects = entities.Adapt<ICollection<TDataObject>>();

        return ServiceResult<IEnumerable<TDataObject>>.Ok(dataObjects);
    }

    public async Task<ServiceResult<TDataObject>> GetByIdAsync(TId id, IncludeOptions<TEntity>? includeOptions = null)
    {
        TEntity? entity = await this.repositoryBase.GetByIdAsync(id, includeOptions);

        return entity != null
            ? ServiceResult<TDataObject>.Ok(entity.Adapt<TDataObject>())
            : ServiceResult<TDataObject>.Fail(ServiceResultStatus.NotFound, [$"{typeof(TEntity)} not found. Id: {id}"]);
    }

    public async Task<ServiceResult<PaginationResult<TDataObject>>> GetPagedAllAsync(int pageNumber, int pageSize, IncludeOptions<TEntity>? includeOptions = null)
    {
        PaginationResult<TEntity> paginationResult = await this.repositoryBase.GetPagedAllAsync(pageNumber, pageSize, includeOptions: includeOptions);
        PaginationResult<TDataObject> paginationResultToReturn = new(paginationResult.Items.Adapt<ICollection<TDataObject>>(), paginationResult.Pagination);

        return ServiceResult<PaginationResult<TDataObject>>.Ok(paginationResultToReturn);
    }

    public async Task<ServiceResult> IsExistsByIdAsync(TId id)
    {
        return await this.repositoryBase.IsExistsByIdAsync(id)
            ? ServiceResult.Ok()
            : ServiceResult.Fail(ServiceResultStatus.NotFound, [$"{typeof(TEntity)} not found. Id: {id}"]);
    }

    public async Task<ServiceResult> UpdateByIdAsync<TUpdateDataObject>(TId id, TUpdateDataObject updateDataObject) where TUpdateDataObject : UpdateDataObjectBase
    {
        TEntity? entityToUpdate = await this.repositoryBase.GetByIdAsync(id);

        if (entityToUpdate != null)
        {
            /* Attach the entity to the change tracker before updating or mapping data
             * to ensure that only modified values are persisted. */
            this.repositoryBase.Attach(entityToUpdate);
            this.mapper.Map(updateDataObject, entityToUpdate);
            await this.repositoryBase.SaveChangesAsync();

            return ServiceResult.Ok();
        }

        return ServiceResult.Fail(ServiceResultStatus.NotFound, [$"{typeof(TEntity)} not found. Id: {id}"]);
    }
}
