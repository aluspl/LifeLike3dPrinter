#region Using(s)

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Database.Repositories.Interfaces;

public interface IReadOnlyRepository<TBaseEntity, TDbContext>
    where TBaseEntity : class
    where TDbContext : DbContext, new()
{
    Task<IReadOnlyList<TBaseEntity>> ListAsync(Expression<Func<TBaseEntity, bool>> spec);

    Task<int> CountAsync(Expression<Func<TBaseEntity, bool>> spec);

    Task<TResult> MaxAsync<TResult>(Expression<Func<TBaseEntity, bool>> spec, Expression<Func<TBaseEntity, TResult>> selector);

    Task<bool> AnyAsync(Expression<Func<TBaseEntity, bool>> spec);

    Task<TBaseEntity> FirstOrDefaultAsync(Expression<Func<TBaseEntity, bool>> spec);
}