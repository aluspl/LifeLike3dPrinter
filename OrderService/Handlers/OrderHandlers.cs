using Database.Infrastructure.Repositories.Interfaces;
using Domain.Order;
using Microsoft.Extensions.Logging;
using OrderService.Models.Orders;
using PrinterService.Handlers;
using Wolverine.Attributes;

namespace OrderService.Handlers;

[WolverineHandler]
public static class OrderHandlers
{
    public static async Task<OrderCreated> HandleAsync(CreateOrder command, IRepository<Order> repository,
        ILogger logger)
    {
        var order = new Order(command.price, command.cost, command.fileName);
        await repository.AddAsync(order);

        logger.LogInformation("Order created {@command}", command);
        return new OrderCreated(order.Id);
    }
    
    public static async Task<OrderUpdated> HandleAsync(UpdateOrder command, IRepository<Order> repository,
        ILogger logger)
    {
        var order = await repository.FirstOrDefaultAsync(x => x.Id == command.id) ??
                    throw new NotFoundException("Order not found");

        order.Update(command.price, command.cost);
        await repository.UpdateAsync(order);
        logger.LogInformation("Order updated {@command}", command);
        return new OrderUpdated(order.Id, order.Updated);
    }
    
    public static async Task<FilamentAddedToOrder> HandleAsync(AddFilamentToOrder command, IRepository<Order> repository,
        ILogger logger)
    {
        var order = await repository.FirstOrDefaultAsync(x => x.Id == command.orderId) ??
                    throw new NotFoundException("Order not found");

        order.AddFilament(command.filamentId, command.weight);
        await repository.UpdateAsync(order);
        logger.LogInformation("Order updated {@command}", command);
        return new FilamentAddedToOrder(order.Id, order.Updated);
    }
}
