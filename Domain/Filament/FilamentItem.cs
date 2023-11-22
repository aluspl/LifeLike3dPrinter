namespace Domain.Filament;

public class FilamentItem : IBaseEntity
{
    public FilamentItem()
    {
    }
    
    public FilamentItem(decimal price, DateTime purchaseDate)
    {
        Price = price;
        Bought = purchaseDate;
    }

    public Guid Id { get; set; }
    
    public DateTime? Updated { get; set; }

    public DateTime Created { get; set; }
    
    public DateTime Bought { get; set; }
    
    public decimal Price { get; set; }
    
    public Guid FilamentId { get; set; }
}