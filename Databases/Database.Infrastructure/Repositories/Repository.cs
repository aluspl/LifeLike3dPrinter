#region Using(s)

using Database.Infrastructure.Context;
using Database.Infrastructure.Repositories.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Database.Infrastructure.Repositories;

public class Repository<TBaseEntity> : ReadOnlyRepository<TBaseEntity>, IRepository<TBaseEntity>
    where TBaseEntity : class
{
    #region Constructor(s)

    public Repository(EFContext context)
        : base(context)
    {
    }

    #endregion

    #region Set

    public async Task<TBaseEntity> AddAsync(TBaseEntity entity)
    {
        Entities.Add(entity);

        await Context.SaveChangesAsync();

        return entity;
    }

    public async Task<IEnumerable<TBaseEntity>> AddRangeAsync(IEnumerable<TBaseEntity> entities)
    {
        Entities.AddRange(entities);

        await Context.SaveChangesAsync();

        return entities;
    }

    public Task<TBaseEntity> AddOrUpdateAsync(TBaseEntity entity)
    {
        if (Context.Entry(entity).IsKeySet)
        {
            return UpdateAsync(entity);
        }
        else
        {
            return AddAsync(entity);
        }
    }

    public async Task<TBaseEntity> UpdateAsync(TBaseEntity entity, bool saveChanges = true)
    {
        Context.Entry(entity).State = EntityState.Modified;
        Context.Update(entity);

        if(entity is IBaseEntity baseEntity)
        {
            baseEntity.Updated = DateTime.UtcNow;
        }

        if (saveChanges)
        {
            await Context.SaveChangesAsync();
        }

        return entity;
    }

    public async Task<IEnumerable<TBaseEntity>> UpdateRangeAsync(IEnumerable<TBaseEntity> entities)
    {
        foreach (var entity in entities)
        {
            await UpdateAsync(entity, saveChanges: false);
        }

        await Context.SaveChangesAsync();

        return entities;
    }

    public async Task DeleteAsync(TBaseEntity entity)
    {
        Entities.Remove(entity);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(List<TBaseEntity> list)
    {
        Entities.RemoveRange(list);
        await Context.SaveChangesAsync();
    }

    #endregion
}