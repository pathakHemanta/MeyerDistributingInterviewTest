using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace InterviewTest
{
    public class ConnectionTest
    {
        public static void Connection()
        {
            string connectionString = "Server=localhost;User ID=root;Password=Kaisenjujutsu@101010;Database=interviewtest;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                var query = "insert into orders (orderNumber, customerName, productNumber, orderDate) values (@orderNumber, @customerName, @productNumber, @orderDate)";
                string orderNumber = "MyOrder123";
                string customerName = "Some Customer";
                string productNumber = "This product";
                string orderDate = "Some Date";
                var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@orderNumber", orderNumber);
                command.Parameters.AddWithValue("@customerName", customerName);
                command.Parameters.AddWithValue("@productNumber", productNumber);
                command.Parameters.AddWithValue("@orderDate", orderDate);

                int rowsAffected = command.ExecuteNonQuery();

                Console.WriteLine($"Rows Affected: {rowsAffected}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
    }
}
