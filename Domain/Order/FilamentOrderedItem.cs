namespace Domain.Order;

public class FilamentOrderedItem
{
    public FilamentOrderedItem()
    {
    }
    
    public FilamentOrderedItem(Guid filamentId, int weight)
    {
        Weight = weight;
        FilamentId = filamentId;
    }

    public Guid FilamentId { get; set; }

    public int Weight { get; set; }
}