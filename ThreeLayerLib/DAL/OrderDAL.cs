using Persistence;
using MySqlConnector;
namespace DAL;

public class OrderDAL
{
    private string query = "";
    private MySqlConnection connection = DbConfig.GetConnection();
    private OrderDetailsDAL orderDetails = new OrderDetailsDAL();
    public bool InsertOrder(Order order)
    {
        try
        {
            query = @"INSERT INTO `Orders`(ID, User_ID, CreationTime) VALUES (@id, @userid, CURRENT_TIMESTAMP());";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", order.ID);
            command.Parameters.AddWithValue("@userid", order.UserID);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }
    public bool UpdateOrderPaymentMethod(int orderID, string paymentMethod)
    {
        try
        {
            query = @"UPDATE Orders SET PaymentMethod = @paymentmethod WHERE ID = @orderid;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@paymentmethod", paymentMethod);
            command.Parameters.AddWithValue("@orderid", orderID);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }
    public List<Order> GetOrders()
    {
        List<Order> orders = new List<Order>();
        Order order = new Order();
        try
        {
            query = @"select ID, User_ID, CreationTime, OrderStatus from Orders;";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                order = GetOrder(reader);
                orders.Add(order);
            }
            reader.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
        return orders;
    }
    public Order GetOrder(MySqlDataReader reader)
    {
        Order order = new Order();
        order.ID = reader.GetInt32("ID");
        order.UserID = reader.GetInt32("User_ID");
        order.CreationTime = reader.GetDateTime("CreationTime");
        order.Status = reader.GetString("OrderStatus");
        return order;
    }
    public bool UpdateOrderStatus(int orderID, string newOrderStatus)
    {
        List<OrderDetails> orders = new List<OrderDetails>();
        OrderDetails ordDtls = new OrderDetails();
        try
        {
            query = @"Update Orders set OrderStatus=@neworderstatus where ID=@orderid;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@orderid", orderID);
            command.Parameters.AddWithValue("@neworderstatus", newOrderStatus);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }
}