namespace Persistence;

public class Product
{
    public int ID { get; set; }
    public string Name { get; set; } = "Unprocessed";
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}