#region Using(s)

using Database.Infrastructure.Context;
using Database.Infrastructure.Repositories;
using Database.Infrastructure.Repositories.Interfaces;
using Database.Migrations;
using Microsoft.EntityFrameworkCore;
using Wolverine.EntityFrameworkCore;

#endregion

namespace Printer.Extensions;

public static class DiExtensions
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepository<>));
        return services;
    }

    public static IServiceCollection UseDatabase(this IServiceCollection services,
        string connectionString, int commandTimeout = 30)
    {
        // If you're okay with this, this will register the DbContext as normally,
        // but make some Wolverine specific optimizations at the same time
        var assemblyFullName = typeof(IDatabaseMigrationsAssembly).Assembly.FullName;
        services.AddDbContextWithWolverineIntegration<EFContext>(x =>
        {
            x.UseNpgsql(connectionString,
                builder =>
                {
                    builder
                        .MigrationsAssembly(assemblyFullName)
                        .CommandTimeout(commandTimeout);
                });
        }, "wolverine");

        services.AddScoped<DbContext, EFContext>();
        services.RegisterRepositories();
        
        return services;
    }
}