using System.Linq.Expressions;
using Commons.Exceptions;
using Database.Infrastructure.Repositories.Interfaces;
using Domain.Filament;
using Microsoft.Extensions.Logging;
using PrinterService.Extensions;
using PrinterService.Models.Filament;
using Wolverine.Attributes;

namespace PrinterService.Handlers;

[WolverineHandler]
public static class FilamentQueryHandlers
{
    public static async Task<FilamentModel> HandleAsync(QueryFilamentItem command, IRepository<Filament> repository)
    {
        var filament = await repository.FirstOrDefaultAsync(x => x.Id == command.id);
        if (filament == null)
        {
            throw new NotFoundException("Filament not found");
        }

        return filament.Map();
    }

    public static async Task<IEnumerable<FilamentModel>> HandleAsync(QueryFilamentList command, IRepository<Filament> repository)
    {
        var query = BuildQuery(command);

        var filaments = await repository.ListAsync(query);
        return filaments.Select(filament => filament.Map());
    }

    private static Expression<Func<Filament, bool>> BuildQuery(QueryFilamentList command)
    {
        Expression<Func<Filament, bool>> query;

        if (command.active)
        {
            query = x => x.Weight - x.Used > 0;
        }
        else
        {
            query = x => true;
        }

        return query;
    }
}