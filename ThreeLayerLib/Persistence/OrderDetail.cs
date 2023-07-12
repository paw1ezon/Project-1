namespace Persistence;

public class OrderDetails
{
    public int OrderID{get;set;}
    public string ProductName{get;set;} = "Unprocessed";
    public int ProductQuantity{get;set;}
    public decimal Totalprice{get;set;} 
}