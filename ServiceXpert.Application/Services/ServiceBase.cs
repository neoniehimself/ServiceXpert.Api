using FluentBuilder.Core;
using Mapster;
using MapsterMapper;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Repositories.Contracts;
using ServiceXpert.Domain.ValueObjects;
using System.Linq.Expressions;

namespace ServiceXpert.Application.Services;
public abstract class ServiceBase<TId, TEntity, TDataObject> : IServiceBase<TId, TEntity, TDataObject>
    where TEntity : EntityBase
    where TDataObject : DataObjectBase
{
    private readonly IRepositoryBase<TId, TEntity> repositoryBase;
    private readonly IMapper mapper;

    public ServiceBase(IRepositoryBase<TId, TEntity> repositoryBase, IMapper mapper)
    {
        this.repositoryBase = repositoryBase;
        this.mapper = mapper;
    }

    public async Task<TId> CreateAsync<TDataObjectForCreate>(TDataObjectForCreate dataObject)
        where TDataObjectForCreate : DataObjectBase
    {
        TEntity entity = dataObject.Adapt<TEntity>();

        await this.repositoryBase.CreateAsync(entity);
        await this.repositoryBase.SaveChangesAsync();

        return GetId(entity);
    }

    public async Task DeleteByIdAsync(TId id)
    {
        await this.repositoryBase.DeleteByIdAsync(id);
        await this.repositoryBase.SaveChangesAsync();
    }

    public async Task<IEnumerable<TDataObject>> GetAllAsync(Expression<Func<TEntity, bool>>? condition = null,
        IncludeOptions<TEntity>? includeOptions = null)
    {
        IEnumerable<TEntity> entities = await this.repositoryBase.GetAllAsync(condition, includeOptions);
        return entities.Adapt<ICollection<TDataObject>>();
    }

    public async Task<TDataObject?> GetAsync(Expression<Func<TEntity, bool>> condition,
        IncludeOptions<TEntity>? includeOptions = null)
    {
        TEntity? entity = await this.repositoryBase.GetAsync(condition, includeOptions);
        return entity?.Adapt<TDataObject>();
    }

    public async Task<TDataObject?> GetByIdAsync(TId entityId, IncludeOptions<TEntity>? includeOptions = null)
    {
        TEntity? entity = await this.repositoryBase.GetByIdAsync(entityId, includeOptions);
        return entity?.Adapt<TDataObject>();
    }

    public async Task<(IEnumerable<TDataObject>, Pagination)> GetPagedAllAsync(int pageNumber = 1, int pageSize = 10,
        Expression<Func<TEntity, bool>>? condition = null, IncludeOptions<TEntity>? includeOptions = null)
    {
        (IEnumerable<TEntity> entities, Pagination pagination) =
            await this.repositoryBase.GetPagedAllAsync(pageNumber, pageSize, condition, includeOptions);

        // Use ICollection instead of IEnumerable to materialize object (required for Mapster)
        return (entities.Adapt<ICollection<TDataObject>>(), pagination);
    }

    public async Task<bool> IsExistsByIdAsync(TId id)
    {
        return await this.repositoryBase.IsExistsByIdAsync(id);
    }

    public async Task UpdateByIdAsync<TDataObjectForUpdate>(TId id, TDataObjectForUpdate dataObject)
            where TDataObjectForUpdate : DataObjectBase
    {
        TEntity? entityToUpdate = await this.repositoryBase.GetByIdAsync(id);

        if (entityToUpdate != null)
        {
            this.repositoryBase.Attach(entityToUpdate);
            this.mapper.Map(dataObject, entityToUpdate);
            await this.repositoryBase.SaveChangesAsync();
        }
    }

    protected static TId GetId(TEntity entity)
    {
        var propId = typeof(TEntity).GetProperty($"{typeof(TEntity).Name}Id");

        var propIdValue = propId != null ? propId.GetValue(entity) : throw new NullReferenceException(nameof(propId));

        return propIdValue != null && propId.GetValue(entity) != null
            ? (TId)propId.GetValue(entity)!
            : throw new NullReferenceException($"{typeof(TEntity).Name}Id is null.");
    }
}
