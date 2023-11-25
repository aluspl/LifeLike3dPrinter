using Database.Infrastructure.Repositories.Interfaces;
using Domain.Order;
using OrderService.Extensions;
using OrderService.Models.Orders;
using PrinterService.Handlers;
using Wolverine.Attributes;

namespace OrderService.Handlers;

[WolverineHandler]
public class OrderQueryHandlers
{
    public static async Task<OrderModel> HandleAsync(QueryOrder command, IRepository<Order> repository)
    {
        var order = await repository.FirstOrDefaultAsync(x => x.Id == command.id) ??
                    throw new NotFoundException("Order not found");
        return order.Map();
    }

    public static async Task<IEnumerable<OrderModel>> HandleAsync(QueryOrderList command,
        IRepository<Order> repository)
    {
        var order = await repository.ListAsync(x => true);
        return order.Select(filament => filament.Map());
    }
}