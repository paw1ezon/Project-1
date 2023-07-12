using Persistence;
using MySqlConnector;
namespace DAL;

public class OrderDetailsDAL
{
    private string query = "";
    private MySqlConnection connection = DbConfig.GetConnection();
    public bool InsertOrderDetails(List<OrderDetails> orderDetails)
    {
        try
        {
            foreach (OrderDetails ordDetail in orderDetails)
            {
                query = @"INSERT INTO OrderDetails(Order_ID, Product_Name, Product_Quantity) VALUES (@orderid, @productname, @productquantity);";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@orderid", ordDetail.OrderID);
                command.Parameters.AddWithValue("@productname", ordDetail.ProductName);
                command.Parameters.AddWithValue("@productquantity", ordDetail.ProductQuantity);
                MySqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }
    public List<OrderDetails> GetOrderDetailsByID(int orderID)
    {
        List<OrderDetails> orders = new List<OrderDetails>();
        OrderDetails ordDtls = new OrderDetails();
        try
        {
            query = @"select * from OrderDetails where Order_ID=@orderid;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@orderid", orderID);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ordDtls = GetOrderDetails(reader);
                orders.Add(ordDtls);
            }
            reader.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
        return orders;
    }
    public OrderDetails GetOrderDetails(MySqlDataReader reader)
    {
        OrderDetails odrDtls = new OrderDetails();
        odrDtls.OrderID = reader.GetInt32("Order_ID");
        odrDtls.ProductName = reader.GetString("Product_Name");
        odrDtls.ProductQuantity = reader.GetInt32("Product_Quantity");
        return odrDtls;
    }
}