using Domain.Filament;
using PrinterService.Models.Filament;

namespace PrinterService.Extensions;

public static class MapExtensions
{
    public static FilamentModel Map(this Filament filament)
    {
        return new FilamentModel()
        {
            Id = filament.Id,
            Updated = filament.Updated,
            FilamentType = filament.FilamentType,
            MaterialType = filament.MaterialType,
            Brand = filament.Brand,
            Color = filament.Color,
            Weight = filament.Weight,
            Used = filament.Used
        };
    }
}