#region Using(s)

using System.Linq.Expressions;
using Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Database.Repositories;

public class ReadOnlyRepository<TBaseEntity, TDbContext> : IReadOnlyRepository<TBaseEntity, TDbContext>
    where TBaseEntity : class
    where TDbContext : DbContext, new()
{
    #region Constructor(s)

    public ReadOnlyRepository(TDbContext context)
    {
        Context = context;
        Entities = Context.Set<TBaseEntity>();
    }

    #endregion

    #region Protected Properties

    protected TDbContext Context { get; set; }

    protected DbSet<TBaseEntity> Entities { get; set; }

    #endregion

    public virtual async Task<IReadOnlyList<TBaseEntity>> ListAsync(Expression<Func<TBaseEntity, bool>> spec)
    {
        return await Entities
            .Where(spec)
            .ToListAsync();
    }

    public virtual async Task<int> CountAsync(Expression<Func<TBaseEntity, bool>> spec)
    {
        return await Entities.CountAsync(spec);
    }

    public virtual async Task<TResult> MaxAsync<TResult>(Expression<Func<TBaseEntity, bool>> spec, Expression<Func<TBaseEntity, TResult>> selector)
    {
        return await Entities.Where(spec).MaxAsync(selector);
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<TBaseEntity, bool>> spec)
    {
        return await Entities.AnyAsync();
    }

    public virtual async Task<TBaseEntity> FirstOrDefaultAsync(Expression<Func<TBaseEntity, bool>> spec)
    {
        return await Entities.FirstOrDefaultAsync(spec);
    }
}