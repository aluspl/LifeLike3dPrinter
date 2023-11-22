using Oakton.Resources;
using PrinterService;
using Wolverine;
using Wolverine.EntityFrameworkCore;
using Wolverine.FluentValidation;
using Wolverine.Postgresql;

namespace Printer.Extensions;

public static class HostExtensions
{
    public static IHostBuilder UseWolverine(this IHostBuilder host, string connectionString)
    {
        host.UseWolverine(options =>
        {
            options.PersistMessagesWithPostgresql(connectionString, "wolverine");
            // If you're also using EF Core, you may want this as well
            options.UseEntityFrameworkCoreTransactions();
            options.UseFluentValidation();

            options.Discovery.IncludeAssembly(typeof(IPrinterAssembly).Assembly);
            options.Policies.UseDurableLocalQueues();

        });
        
        return host;
    }
}