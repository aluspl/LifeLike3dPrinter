namespace OrderService.Models.Orders;

public record CreateOrder(List<FilamentOrderCreate> filaments, string fileName, string fileUrl, decimal price, decimal cost);

public record UpdateOrder(Guid id, List<FilamentOrderUpdate> filaments, decimal price, decimal cost);
public record FilamentOrderUpdate(Guid id, int beforeWeight, int afterWeight);

public record FilamentOrderCreate(Guid id, int weight);

public record AddFilamentToOrder(Guid orderId, Guid filamentId, int weight);

public record QueryOrder(Guid id);

public record QueryOrderList(bool active = true);