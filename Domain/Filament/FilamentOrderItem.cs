namespace Domain.Filament;

public class FilamentOrderItem
{
    public FilamentOrderItem(Guid orderId, int weight)
    {
        OrderId = orderId;
        Weight = weight;
    }

    public int Weight { get; set; }

    public Guid OrderId { get; set; }
}