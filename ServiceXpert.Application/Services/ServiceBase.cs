using FluentBuilder.Core;
using Mapster;
using MapsterMapper;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Application.Services.Contracts;
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

    public virtual async Task<TId> CreateAsync<TDataObjectForCreate>(TDataObjectForCreate dataObjectForCreate) where TDataObjectForCreate : DataObjectBaseForCreate
    {
        TEntity entity = dataObjectForCreate.Adapt<TEntity>();

        await this.repositoryBase.CreateAsync(entity);
        await this.repositoryBase.SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteByIdAsync(TId id)
    {
        await this.repositoryBase.DeleteByIdAsync(id);
        await this.repositoryBase.SaveChangesAsync();
    }

    public async Task<IEnumerable<TDataObject>> GetAllAsync(IncludeOptions<TEntity>? includeOptions = null)
    {
        IEnumerable<TEntity> entities = await this.repositoryBase.GetAllAsync(includeOptions: includeOptions);
        return entities.Adapt<ICollection<TDataObject>>();
    }

    public async Task<TDataObject?> GetByIdAsync(TId id, IncludeOptions<TEntity>? includeOptions = null)
    {
        TEntity? entity = await this.repositoryBase.GetByIdAsync(id, includeOptions);
        return entity?.Adapt<TDataObject>();
    }

    public async Task<PagedResult<TDataObject>> GetPagedAllAsync(int pageNumber, int pageSize, IncludeOptions<TEntity>? includeOptions = null)
    {
        PagedResult<TEntity> pagedResult = await this.repositoryBase.GetPagedAllAsync(pageNumber, pageSize, includeOptions: includeOptions);

        // Use ICollection instead of IEnumerable to materialize object (required for Mapster)
        return new PagedResult<TDataObject>(pagedResult.Items.Adapt<ICollection<TDataObject>>(), pagedResult.Pagination);
    }

    public async Task<bool> IsExistsByIdAsync(TId id)
    {
        return await this.repositoryBase.IsExistsByIdAsync(id);
    }

    public async Task UpdateByIdAsync<TDataObjectForUpdate>(TId id, TDataObjectForUpdate dataObjectForUpdate) where TDataObjectForUpdate : DataObjectBaseForUpdate
    {
        TEntity? entityToUpdate = await this.repositoryBase.GetByIdAsync(id);

        if (entityToUpdate != null)
        {
            /* Attach the entity to the change tracker before updating or mapping data
             * to ensure that only modified values are persisted. */
            this.repositoryBase.Attach(entityToUpdate);
            this.mapper.Map(dataObjectForUpdate, entityToUpdate);
            await this.repositoryBase.SaveChangesAsync();
        }
    }
}
