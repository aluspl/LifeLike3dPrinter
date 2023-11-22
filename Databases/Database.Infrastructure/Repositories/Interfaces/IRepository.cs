namespace Database.Infrastructure.Repositories.Interfaces;

public interface IRepository<TBaseEntity> : IReadOnlyRepository<TBaseEntity>
    where TBaseEntity : class
{
    #region Set

    Task<TBaseEntity> AddAsync(TBaseEntity entity);

    Task<IEnumerable<TBaseEntity>> AddRangeAsync(IEnumerable<TBaseEntity> entities);

    Task<TBaseEntity> AddOrUpdateAsync(TBaseEntity entity);

    Task<TBaseEntity> UpdateAsync(TBaseEntity entity, bool saveChanges = true);

    Task<IEnumerable<TBaseEntity>> UpdateRangeAsync(IEnumerable<TBaseEntity> entities);

    Task DeleteAsync(TBaseEntity entity);

    Task DeleteAsync(List<TBaseEntity> list);

    #endregion
}