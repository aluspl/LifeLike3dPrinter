using Microsoft.EntityFrameworkCore;

namespace Database.Context;

public class EFContext : DbContext
{
    #region Constructor(s)

    public EFContext(DbContextOptions<EFContext> options)
        : base(options)
    {
    }

    public EFContext()
    {
    }

    #endregion
    
    
    #region Builder

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(IDatabaseAssembly).Assembly);
    }

    #endregion
}