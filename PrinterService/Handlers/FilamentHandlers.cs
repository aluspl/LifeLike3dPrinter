using Commons.Exceptions;
using Database.Infrastructure.Repositories.Interfaces;
using Domain.Filament;
using Microsoft.Extensions.Logging;
using PrinterService.Models.Filament;
using Wolverine.Attributes;

namespace PrinterService.Handlers;

[WolverineHandler]
public class FilamentHandlers
{
    public async Task<FilamentCreated> HandleAsync(CreateFilament command, IRepository<Filament> repository,
        ILogger<FilamentHandlers> logger)
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

    public async Task<FilamentRefilled> HandleAsync(RefillFilament command, IRepository<Filament> repository,
        ILogger<FilamentHandlers> logger)
    {
        var filament = await repository.FirstOrDefaultAsync(x => x.Id == command.Id);
        if (filament == null)
        {
            throw new NotFoundException("Filament not found");
        }

        filament.Refill(command.weight, command.price, command.purchaseDate.ToUniversalTime());

        await repository.UpdateAsync(filament);
        logger.LogInformation("Filament refilled {@command}", command);
        return new FilamentRefilled(filament.Id, filament.Updated);
    }

    public async Task<FilamentModel> LoadAsync(QueryFilament command, IRepository<Filament> repository)
    {
        var filament = await repository.FirstOrDefaultAsync(x => x.Id == command.id);
        if (filament == null)
        {
            throw new NotFoundException("Filament not found");
        }

        return new FilamentModel(
            filament.Id,
            filament.Updated,
            filament.FilamentType,
            filament.MaterialType,
            filament.Brand,
            filament.Color,
            filament.Weight,
            filament.Used);
    }

    public async Task<IEnumerable<FilamentModel>> LoadAsync(QueryFilaments command, IRepository<Filament> repository)
    {
        var filaments = await repository.ListAsync(x => command.created < x.Created);
        return filaments.Select(filament =>
            new FilamentModel(
                filament.Id,
                filament.Updated,
                filament.FilamentType,
                filament.MaterialType,
                filament.Brand,
                filament.Color,
                filament.Weight,
                filament.Used));
    }
}