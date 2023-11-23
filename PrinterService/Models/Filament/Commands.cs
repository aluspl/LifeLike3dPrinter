using Commons.Enums;

namespace PrinterService.Models.Filament;

public record UseFilament(Guid id, int weight, DateTime? usedDate);
public record CreateFilament(string brandName, FilamentType filamentType, MaterialType materialType, string color, int weight, decimal price, DateTime purchaseDate);

public record RefillFilament(Guid Id, int weight, decimal price, DateTime purchaseDate);

public record QueryFilamentItem(Guid id);

public record QueryFilamentList(bool active = true, FilamentType? filamentType = null);