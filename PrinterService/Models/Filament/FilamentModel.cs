using Commons.Enums;

namespace PrinterService.Models.Filament;

public class FilamentModel
{
    public FilamentModel()
    {
    }

    public FilamentModel(
        Guid filamentId, 
        DateTime? filamentUpdated, 
        FilamentType filamentFilamentType,
        MaterialType filamentMaterialType, 
        string filamentBrand, 
        string filamentColor, 
        int filamentWeight,
        int filamentUsed)
    {
        Id = filamentId;
        Updated = filamentUpdated;
        FilamentType = filamentFilamentType;
        MaterialType = filamentMaterialType;
        Brand = filamentBrand;
        Color = filamentColor;
        Weight = filamentWeight;
        Used = filamentUsed;
    }

    public Guid Id { get; set; }
    public DateTime? Updated { get; set; }
    public FilamentType FilamentType { get; set; }
    public MaterialType MaterialType { get; set; }
    public string Brand { get; set; }
    public string Color { get; set; }
    public int Weight { get; set; }

    public int Used { get; set; }
}