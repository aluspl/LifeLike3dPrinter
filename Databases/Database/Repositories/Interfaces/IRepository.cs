using Microsoft.EntityFrameworkCore;

namespace Database.Repositories.Interfaces;

public interface IRepository<TBaseEntity, TDbContext> : IReadOnlyRepository<TBaseEntity, TDbContext>
    where TBaseEntity : class
    where TDbContext : DbContext, new()
{
    #region Set

    Task<TBaseEntity> AddAsync(TBaseEntity entity);

    Task<IEnumerable<TBaseEntity>> AddRangeAsync(IEnumerable<TBaseEntity> entities);

    Task<TBaseEntity> AddOrUpdateAsync(TBaseEntity entity);

    Task<TBaseEntity> UpdateAndSaveAsync(TBaseEntity entity, bool saveChanges = true);

    Task<IEnumerable<TBaseEntity>> UpdateRangeAsync(IEnumerable<TBaseEntity> entities);

    Task DeleteAsync(TBaseEntity entity);

    Task DeleteAsync(List<TBaseEntity> list);

    #endregion
}