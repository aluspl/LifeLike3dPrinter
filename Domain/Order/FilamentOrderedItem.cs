namespace Domain.Order;

public class FilamentOrderedItem
{
    public FilamentOrderedItem()
    {
    }
    
    public FilamentOrderedItem(Guid filamentId, int weight, DateTime usedDate)
    {
        Weight = weight;
        UsedDate = usedDate;
        FilamentId = filamentId;
        Weight = weight;
    }

    public Guid FilamentId { get; set; }

    public DateTime UsedDate { get; set; }

    public int Weight { get; set; }
}