using Persistence;
using MySqlConnector;
namespace DAL;

public class UserDAL
{
    private string query = "";
    private MySqlConnection connection = DbConfig.GetConnection();
    public User GetUserAccount(string userName)
    {
        User user = new User();
        try
        {
            query = @"select * from Users where user_name=@username;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", userName);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                user = GetUser(reader);
            }
            reader.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
        return user;
    }
    public User GetUser(MySqlDataReader reader)
    {
        User user = new User();
        user.ID = reader.GetInt32("ID");
        user.UserName = reader.GetString("User_Name");
        user.Password = reader.GetString("Password");
        user.Role = reader.GetInt32("Role_ID");
        return user;
    }
}