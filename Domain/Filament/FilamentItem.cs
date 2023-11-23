namespace Domain.Filament;

public class FilamentItem
{
    public FilamentItem()
    {
    }
    
    public FilamentItem(decimal price, DateTime purchaseDate)
    {
        Price = price;
        Bought = purchaseDate;
    }

    public DateTime Bought { get; set; }
    
    public decimal Price { get; set; }
}