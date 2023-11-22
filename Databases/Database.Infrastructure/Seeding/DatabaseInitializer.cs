using Database.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Database.Infrastructure.Seeding;

public class DatabaseInitializer(EFContext context, IConfiguration configuration) : IHostedService
{
    public async Task InitializeAsync()
    {
        await context.Database.MigrateAsync();
        await context.SaveChangesAsync();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await InitializeAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}