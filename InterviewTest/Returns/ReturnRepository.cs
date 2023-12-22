using System;
using System.Collections.Generic;
using System.Linq;
using MySqlConnector;

namespace InterviewTest.Returns
{
    public class ReturnRepository
    {
        private List<IReturn> returns;
        private MySqlConnection connection;
        public ReturnRepository(MySqlConnection connection)
        {
            this.connection = connection;
            returns = new List<IReturn>();
        }

        public void Add(IReturn newReturn)
        {
            try
            {
                connection.Open();
                foreach (var product in newReturn.ReturnedProducts)
                {
                    var query = "insert into returns (returnNumber, orderNumber, productNumber) values (@returnNumber, @orderNumber, @productNumber)";
                    string returnNumber = newReturn.ReturnNumber;
                    string orderNumber = newReturn.OriginalOrder.OrderNumber;
                    string productNumber = product.OrderProduct.Product.GetProductNumber();
                    var command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@returnNumber", returnNumber);
                    command.Parameters.AddWithValue("@orderNumber", orderNumber);
                    command.Parameters.AddWithValue("@productNumber", productNumber);

                    int rowsAffected = command.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            returns.Add(newReturn);
        }

        public void Remove(IReturn removedReturn)
        {
            try
            {
                connection.Open();

                var query = "delete from returns where returnNumber = @returnNumber";
                string returnNumber = removedReturn.ReturnNumber;
                var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@returnNumber", returnNumber);

                int rowsAffected = command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            returns = returns.Where(o => !string.Equals(removedReturn.ReturnNumber, o.ReturnNumber)).ToList();
        }

        public List<IReturn> Get()
        {
            return returns;
        }
    }
}
