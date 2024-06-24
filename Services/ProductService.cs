using MySql.Data.MySqlClient;
using System.Collections.Generic;
using sqlapp.Models;

namespace sqlapp.Services
{
    public class ProductService
    {
        private static string db_source = Environment.GetEnvironmentVariable("DB_SOURCE");
        private static string db_user = Environment.GetEnvironmentVariable("DB_USER");
        private static string db_password = Environment.GetEnvironmentVariable("DB_PASSWORD");
        private static string db_database = Environment.GetEnvironmentVariable("DB_DATABASE");

        private MySqlConnection GetConnection()
        {
            var connectionString = $"Server={db_source};Database={db_database};User ID={db_user};Password={db_password};";
            return new MySqlConnection(connectionString);
        }

        public List<Product> GetProducts()
        {
            List<Product> _product_lst = new List<Product>();
            string _statement = "SELECT ProductID, ProductName, Quantity FROM Products";
            MySqlConnection _connection = GetConnection();

            _connection.Open();

            MySqlCommand _sqlcommand = new MySqlCommand(_statement, _connection);

            using (MySqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    Product _product = new Product()
                    {
                        ProductID = _reader.GetInt32("ProductID"),
                        ProductName = _reader.GetString("ProductName"),
                        Quantity = _reader.GetInt32("Quantity")
                    };

                    _product_lst.Add(_product);
                }
            }
            _connection.Close();
            return _product_lst;
        }
    }
}
