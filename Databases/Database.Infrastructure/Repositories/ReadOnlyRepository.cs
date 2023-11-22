#region Using(s)

using System.Linq.Expressions;
using Database.Infrastructure.Context;
using Database.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Database.Infrastructure.Repositories;

public class ReadOnlyRepository<TBaseEntity> : IReadOnlyRepository<TBaseEntity>
    where TBaseEntity : class
{
    #region Constructor(s)

    public ReadOnlyRepository(EFContext context)
    {
        Context = context;
        Entities = Context.Set<TBaseEntity>();
    }

    #endregion

    #region Protected Properties

    protected EFContext Context { get; set; }

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