namespace OrderService.Models.Orders;

public record OrderCreated(Guid id);

public record OrderUpdated(Guid id, DateTime? Updated);

public record FilamentAddedToOrder(Guid OrderId, DateTime? OrderUpdated);
