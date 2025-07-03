using System.Data.SqlClient;
using BlazorProductStore.DAL.Models;

namespace BlazorProductStore.DAL
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Product> GetAll()
        {
            var products = new List<Product>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT ProductId, ProductName, Description, Price, CategoryId FROM Products", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductId = (int)reader["ProductId"],
                            ProductName = (string)reader["ProductName"],
                            Description = reader["Description"] as string,
                            Price = (decimal)reader["Price"],
                            CategoryId = (int)reader["CategoryId"]
                        });
                    }
                }
            }
            return products;
        }

        // Implement GetById, Add, Update, Delete methods similarly...
        // Example for Add:
        public void Add(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "INSERT INTO Products (ProductName, Description, Price, CategoryId) VALUES (@ProductName, @Description, @Price, @CategoryId)";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Description", (object)product.Description ?? DBNull.Value);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}