using Commons.Enums;

namespace PrinterService.Models.Filament;

public class FilamentModel
{
    public Guid Id { get; set; }
    public DateTime? Updated { get; set; }
    public FilamentType FilamentType { get; set; }
    public MaterialType MaterialType { get; set; }
    public string Brand { get; set; }
    public string Color { get; set; }
    public int Weight { get; set; }

    public int Used { get; set; }
}