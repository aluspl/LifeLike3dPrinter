#region Using(s)

using System.Linq.Expressions;

#endregion

namespace Database.Infrastructure.Repositories.Interfaces;

public interface IReadOnlyRepository<TBaseEntity>
    where TBaseEntity : class
{
    Task<IReadOnlyList<TBaseEntity>> ListAsync(Expression<Func<TBaseEntity, bool>> spec);

    Task<int> CountAsync(Expression<Func<TBaseEntity, bool>> spec);

    Task<TResult> MaxAsync<TResult>(Expression<Func<TBaseEntity, bool>> spec, Expression<Func<TBaseEntity, TResult>> selector);

    Task<bool> AnyAsync(Expression<Func<TBaseEntity, bool>> spec);

    Task<TBaseEntity> FirstOrDefaultAsync(Expression<Func<TBaseEntity, bool>> spec);
}