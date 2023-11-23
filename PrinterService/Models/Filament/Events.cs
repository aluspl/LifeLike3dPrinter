namespace PrinterService.Models.Filament;

public record FilamentCreated(Guid Id);

public record FilamentRefilled(Guid id, DateTime? updated, int weight);

public record FilamentUsed(Guid id, DateTime? updated, int left);