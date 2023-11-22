using Wolverine;

namespace PrinterService.Models.Filament;

public record FilamentDetailed(Guid Id, DateTime? Updated) : IMessage;