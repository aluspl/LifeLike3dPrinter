namespace Domain.Order;

public class Order : IBaseEntity
{
    public Order()
    {
        OrderedItems = new List<FilamentOrderedItem>();
    }

    public Order(decimal price, decimal cost, string fileName, string fileUrl=null) : base()
    {
        Price = price;
        Cost= cost;
        Filename = fileName;
        FileUrl = fileUrl;
    }

    public Guid Id { get; set; }
 
    public DateTime? Updated { get; set; }
    
    public DateTime Created { get; set; }
    
    public string Filename { get; set; }

    public string FileUrl { get; set; }

    public decimal Cost { get; set; }
    
    public decimal Price { get; set; }

    public string Status { get; set; }
    
    public List<FilamentOrderedItem> OrderedItems { get; set; }
    
    public void AddFilament(Guid filamentId, int weight)
    {
        OrderedItems.Add(new FilamentOrderedItem(filamentId, weight));
    }

    public void Update(decimal price, decimal cost)
    {
        Price = price;
        Cost = cost;
    }
}