namespace PrinterService.Handlers;

public record RefillFilament(Guid Id, int weight, decimal price, DateTime purchaseDate);