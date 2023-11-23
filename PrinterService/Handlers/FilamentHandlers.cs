using Commons.Exceptions;
using Database.Infrastructure.Repositories.Interfaces;
using Domain.Filament;
using Microsoft.Extensions.Logging;
using PrinterService.Models.Filament;
using Wolverine.Attributes;

namespace PrinterService.Handlers;

[WolverineHandler]
public static class FilamentHandlers
{
    public static async Task<FilamentCreated> HandleAsync(CreateFilament command, IRepository<Filament> repository, ILogger logger)
    {
        var filament = await repository.FirstOrDefaultAsync(x =>
            x.Brand == command.brandName && x.Color == command.color && x.FilamentType == command.filamentType &&
            x.MaterialType == command.materialType);
        if (filament != null)
        {
            throw new BusinessException("Filament already exists");
        }

        filament = new Filament(command.brandName, command.color, command.filamentType, command.materialType);
        filament.Refill(command.weight, command.price, command.purchaseDate.ToUniversalTime());
        await repository.AddAsync(filament);

        logger.LogInformation("Filament created {@command}", command);
        return new FilamentCreated(filament.Id);
    }

    public static async Task<FilamentRefilled> HandleAsync(RefillFilament command, IRepository<Filament> repository, ILogger logger)
    {
        var filament = await repository.FirstOrDefaultAsync(x => x.Id == command.Id) ?? throw new NotFoundException("Filament not found");
        filament.Refill(command.weight, command.price, command.purchaseDate.ToUniversalTime());

        await repository.UpdateAsync(filament);
        logger.LogInformation("Filament refilled {@command}", command);
        return new FilamentRefilled(filament.Id, filament.Updated, filament.Weight);
    }
    
    public static async Task<FilamentUsed> HandleAsync(UseFilament command, IRepository<Filament> repository, ILogger logger)
    {
        var filament = await repository.FirstOrDefaultAsync(x => x.Id == command.id) ??
                       throw new NotFoundException("Filament not found");

        filament.Use(command.weight, command.usedDate);

        await repository.UpdateAsync(filament);
        logger.LogInformation("Filament Used {@command}", command);
        return new FilamentUsed(filament.Id, filament.Updated, filament.Left);
    }
}