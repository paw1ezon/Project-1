namespace Persistence;

public class Order{
    public int ID{get;set;}
    public int UserID{get;set;}
    public string PaymentMethod{get;set;} = "cash";
    public DateTime CreationTime{get;set;}
    public string Status{get;set;} = "dafault";
}