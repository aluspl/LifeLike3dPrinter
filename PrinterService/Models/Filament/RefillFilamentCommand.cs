namespace PrinterService.Handlers;

public record RefillFilamentCommand(Guid Id, int weight, decimal price, DateTime purchaseDate);