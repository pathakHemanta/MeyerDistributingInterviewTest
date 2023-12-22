using System;
using System.Collections.Generic;
using System.Linq;
using MySqlConnector;

namespace InterviewTest.Orders
{
    public class OrderRepository
    {
        private List<IOrder> orders;
        private MySqlConnection connection;

        public OrderRepository(MySqlConnection connection)
        {
            this.connection = connection;
            orders = new List<IOrder>();
        }

        public void Add(IOrder newOrder)
        {
            try
            {
                connection.Open();
                foreach (var product in newOrder.Products)
                {
                    var query = "insert into orders (orderNumber, customerName, productNumber, orderDate) values (@orderNumber, @customerName, @productNumber, @orderDate)";
                    string orderNumber = newOrder.OrderNumber;
                    string customerName = newOrder.Customer.GetName();
                    string productNumber = product.Product.GetProductNumber();
                    string orderDate = newOrder.OrderDate;
                    var command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@orderNumber", orderNumber);
                    command.Parameters.AddWithValue("@customerName", customerName);
                    command.Parameters.AddWithValue("@orderDate", orderDate);
                    command.Parameters.AddWithValue("@productNumber", productNumber);

                    int rowsAffected = command.ExecuteNonQuery();

                    //Console.WriteLine($"Rows Affected: {rowsAffected}");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            orders.Add(newOrder);
        }

        public void Remove(IOrder removedOrder)
        {
            try
            {
                connection.Open();

                var query = "delete from orders where orderNumber = @orderNumber";
                string orderNumber = removedOrder.OrderNumber;
                var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@orderNumber", orderNumber);

                int rowsAffected = command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            orders = orders.Where(o => !string.Equals(removedOrder.OrderNumber, o.OrderNumber)).ToList();
        }

        public List<IOrder> Get()
        {
            List<IOrder> databaseOrders = new();
            try
            {
                connection.Open();

                var query = "select * from orders";
                var command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int fieldCount = reader.FieldCount;

                    for (int i = 0; i < fieldCount; i++)
                    {
                        string fieldName = reader.GetName(i);
                        object fieldValue = reader.GetValue(i);

                        Console.WriteLine($"{fieldName}: {fieldValue}");
                    }

                    Console.WriteLine();
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return orders;
        }
    }
}
