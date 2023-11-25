using Domain.Order;
using OrderService.Models.Orders;

namespace OrderService.Extensions;

public static class MapExtensions
{
    public static OrderModel Map(this Order filament)
    {
        return new OrderModel(
            filament.Id,
            filament.Created,
            filament.Updated,
            filament.Price,
            filament.Status,
            filament.Filename,
            filament.FileUrl);
    }
}