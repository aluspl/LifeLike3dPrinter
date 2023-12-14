using Domain.Order;

namespace OrderService.Models.Orders;

public class OrderModel
{
    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public Guid Id { get; set; }

    public string FileUrl { get; set; }

    public string Status { get; set; }

    public decimal Price { get; set; }

    public string Filename { get; set; }
}