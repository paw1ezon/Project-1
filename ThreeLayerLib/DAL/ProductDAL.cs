using Persistence;
using MySqlConnector;
namespace DAL;

public class ProductDAL
{
    private string query = "";
    private MySqlConnection connection = DbConfig.GetConnection();
    public List<Product> GetProducts()
    {
        Product product = new Product();
        List<Product> products = new List<Product>();
        try
        {
            query = @"select * from Products;";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                product = GetProduct(reader);
                products.Add(product);
            }
            reader.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
        return products;
    }
    public Product GetProductByID(int productID)
    {
        Product product = new Product();
        try
        {
            query = @"select * from Products WHERE ID = @productid;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@productid", productID);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                product = GetProduct(reader);
            }
            reader.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
        return product;
    }
    public Product GetProduct(MySqlDataReader reader)
    {
        Product product = new Product();
        product.ID = reader.GetInt32("ID");
        product.Name = reader.GetString("Product_Name");
        product.Quantity = reader.GetInt32("Quantity");
        product.Price = reader.GetDecimal("Price");
        return product;
    }
}