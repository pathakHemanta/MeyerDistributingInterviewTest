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

                // Insert each product in return as a row in the database table
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

                    //int rowsAffected = command.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception)
            {

            }
            returns.Add(newReturn);
        }

        public void Remove(IReturn removedReturn)
        {
            try
            {
                connection.Open();

                // deletes all product with the given product number from the database
                var query = "delete from returns where returnNumber = @returnNumber";
                string returnNumber = removedReturn.ReturnNumber;
                var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@returnNumber", returnNumber);

                //int rowsAffected = command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception)
            {

            }
            returns = returns.Where(o => !string.Equals(removedReturn.ReturnNumber, o.ReturnNumber)).ToList();
        }

        public List<IReturn> Get()
        {
            try
            {
                connection.Open();

                // Gets all the orders and products from the database
                var query = "select * from returns";
                var command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int fieldCount = reader.FieldCount;

                    for (int i = 0; i < fieldCount; i++)
                    {
                        string fieldName = reader.GetName(i);
                        object fieldValue = reader.GetValue(i);

                        //Console.WriteLine($"{fieldName}: {fieldValue}");
                    }

                    //Console.WriteLine();
                }

                connection.Close();
            }
            catch (Exception)
            {

            }
            return returns;
        }
    }
}
