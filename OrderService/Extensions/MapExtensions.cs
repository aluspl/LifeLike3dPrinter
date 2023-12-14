using Domain.Order;
using OrderService.Models.Orders;

namespace OrderService.Extensions;

public static class MapExtensions
{
    public static OrderModel Map(this Order filament)
    {
        return new OrderModel()
        {
            Id = filament.Id,
            Updated = filament.Updated,
            Created = filament.Created,
            FileUrl = filament.FileUrl,
            Status = filament.Status,
            Price = filament.Price,
            Filename = filament.Filename
        };
    }
}