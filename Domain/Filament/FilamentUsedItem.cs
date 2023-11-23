namespace Domain.Filament;

public class FilamentUsedItem
{
    public FilamentUsedItem()
    {
    }
    
    public FilamentUsedItem(int weight, DateTime usedDate)
    {
        Weight = weight;
        UsedDate = usedDate;
    }

    public DateTime UsedDate { get; set; }

    public int Weight { get; set; }
}