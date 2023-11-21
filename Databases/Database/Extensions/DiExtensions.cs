#region Using(s)

using Database.Interfaces;
using Database.Repositories;
using Database.Repositories.Interfaces;
using Database.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

#endregion

namespace Database.Extensions;

public static class DiExtensions
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>));
        return services;
    }
    
    public static IServiceCollection UseDatabase<T, TMigration>(this IServiceCollection services, string connectionString, int commandTimeout = 30)
        where T : DbContext
    {
        return services.ConfigureDatabase<T, TMigration>(connectionString, commandTimeout);
    }

    private static IServiceCollection ConfigureDatabase<T, TMigration>(this IServiceCollection services, string connectionString, int commandTimeout = 30)
        where T : DbContext
    {
        services.AddDbContext<T>((serviceProvider, databaseContextOptionBuilder) =>
        {
            var registeredInterceptors = serviceProvider.GetRequiredService<IEnumerable<IInterceptor>>();

            databaseContextOptionBuilder.UseNpgsql(
                    connectionString,
                    builder => builder
                        .MigrationsAssembly(typeof(TMigration).Assembly.FullName)
                        .CommandTimeout(commandTimeout))
                .AddInterceptors(registeredInterceptors)
                .ConfigureWarnings(wcb =>
                {
                    wcb.Log(
                        (CoreEventId.RowLimitingOperationWithoutOrderByWarning, LogLevel.Information),
                        (CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning, LogLevel.Information));
                });
        });

        services.AddScoped<DbContext, T>();
        services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();

        return services;
    }
}