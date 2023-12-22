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
                var command = new MySqlCommand("select * from orders", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader.FieldCount);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: { ex.Message}");
            }
           
        }
    }
}
