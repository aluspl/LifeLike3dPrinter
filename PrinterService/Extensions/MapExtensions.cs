using Domain.Filament;
using PrinterService.Models.Filament;

namespace PrinterService.Extensions;

public static class MapExtensions
{
    public static FilamentModel Map(this Filament filament)
    {
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
}