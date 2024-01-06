using System;
using MySqlConnector;

namespace InterviewTest
{
    public class DatabaseConnection
    {
        private string connectionString = "Server=localhost;User ID=root;Password=Kaisenjujutsu@101010;Database=interviewtest;";
        public void Connection()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            
            try
            {
                connection.Open();
                var command = new MySqlCommand("select * from orders", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int fieldCount = reader.FieldCount;

                    for(int i = 0; i < fieldCount; i++)
                    {
                        string fieldName = reader.GetName(i);
                        object value = reader.GetValue(i);

                        Console.Write($" {fieldName}: {value} ");
                    }
                    Console.WriteLine();
                }

                connection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: { ex.Message}");
            }
           
        }
    }
}
