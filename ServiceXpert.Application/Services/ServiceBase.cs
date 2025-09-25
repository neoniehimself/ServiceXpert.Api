using FluentBuilder.Core;
using Mapster;
using MapsterMapper;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Application.Shared;
using ServiceXpert.Application.Shared.Enums;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Repositories;
using ServiceXpert.Domain.Shared.ValueObjects;

namespace ServiceXpert.Application.Services;
public abstract class ServiceBase<TId, TEntity, TDataObject> : IServiceBase<TId, TEntity, TDataObject> where TEntity : EntityBase<TId> where TDataObject : DataObjectBase<TId>
{
    private readonly IRepositoryBase<TId, TEntity> repositoryBase;
    private readonly IMapper mapper;

    public ServiceBase(IRepositoryBase<TId, TEntity> repositoryBase, IMapper mapper)
    {
        this.repositoryBase = repositoryBase;
        this.mapper = mapper;
    }

    public virtual async Task<Result<TId>> CreateAsync<TDataObjectForCreate>(TDataObjectForCreate dataObjectForCreate) where TDataObjectForCreate : DataObjectBaseForCreate
    {
        TEntity entity = dataObjectForCreate.Adapt<TEntity>();

        await this.repositoryBase.CreateAsync(entity);
        await this.repositoryBase.SaveChangesAsync();

        return Result<TId>.Ok(entity.Id);
    }

    public async Task<Result> DeleteByIdAsync(TId id)
    {
        await this.repositoryBase.DeleteByIdAsync(id);
        await this.repositoryBase.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result<IEnumerable<TDataObject>>> GetAllAsync(IncludeOptions<TEntity>? includeOptions = null)
    {
        IEnumerable<TEntity> entities = await this.repositoryBase.GetAllAsync(includeOptions: includeOptions);
        ICollection<TDataObject> dataObjects = entities.Adapt<ICollection<TDataObject>>();

        return Result<IEnumerable<TDataObject>>.Ok(dataObjects);
    }

    public async Task<Result<TDataObject>> GetByIdAsync(TId id, IncludeOptions<TEntity>? includeOptions = null)
    {
        TEntity? entity = await this.repositoryBase.GetByIdAsync(id, includeOptions);

        return entity != null
            ? Result<TDataObject>.Ok(entity.Adapt<TDataObject>())
            : Result<TDataObject>.Fail(ResultStatus.NotFound, [$"{typeof(TEntity)} not found. Id: {id}"]);
    }

    public async Task<Result<PagedResult<TDataObject>>> GetPagedAllAsync(int pageNumber, int pageSize, IncludeOptions<TEntity>? includeOptions = null)
    {
        PagedResult<TEntity> pagedResult = await this.repositoryBase.GetPagedAllAsync(pageNumber, pageSize, includeOptions: includeOptions);
        PagedResult<TDataObject> pagedResultToReturn = new(pagedResult.Items.Adapt<ICollection<TDataObject>>(), pagedResult.Pagination);

        return Result<PagedResult<TDataObject>>.Ok(pagedResultToReturn);
    }

    public async Task<Result> IsExistsByIdAsync(TId id)
    {
        return await this.repositoryBase.IsExistsByIdAsync(id)
            ? Result.Ok()
            : Result.Fail(ResultStatus.NotFound, [$"{typeof(TEntity)} not found. Id: {id}"]);
    }

    public async Task<Result> UpdateByIdAsync<TDataObjectForUpdate>(TId id, TDataObjectForUpdate dataObjectForUpdate) where TDataObjectForUpdate : DataObjectBaseForUpdate
    {
        TEntity? entityToUpdate = await this.repositoryBase.GetByIdAsync(id);

        if (entityToUpdate != null)
        {
            /* Attach the entity to the change tracker before updating or mapping data
             * to ensure that only modified values are persisted. */
            this.repositoryBase.Attach(entityToUpdate);
            this.mapper.Map(dataObjectForUpdate, entityToUpdate);
            await this.repositoryBase.SaveChangesAsync();

            return Result.Ok();
        }

        return Result.Fail(ResultStatus.NotFound, [$"{typeof(TEntity)} not found. Id: {id}"]);
    }
}
