using Commons.Enums;

namespace Domain.Filament;

public class Filament : IBaseEntity
{
    public Filament()
    {
    }
    
    public Filament(string brand, string color, FilamentType filamentType, MaterialType materialType)
    {
        Brand = brand;
        Color = color;
        FilamentType = filamentType;
        MaterialType = materialType;
    }

    public Guid Id { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public FilamentType FilamentType { get; set; }

    public MaterialType MaterialType { get; set; }

    public string Brand { get; set; }

    public string Color { get; set; }

    public int Weight { get; set; }
    
    public int Used { get; set; }
    
    public List<FilamentItem> Items { get; set; }

    public void Refill(int weight, decimal price, DateTime purchaseDate)
    {
        Weight += weight;
        
        Items ??= new List<FilamentItem>();
        Items.Add(new FilamentItem(price, purchaseDate));
    }
}