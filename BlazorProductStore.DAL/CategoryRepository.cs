using System.Data.SqlClient;
using BlazorProductStore.DAL.Models;

namespace BlazorProductStore.DAL
{
    public class CategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // CREATE
        public void Add(Category category)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "INSERT INTO Categories (CategoryName) VALUES (@CategoryName)";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // READ (Get one by ID)
        public Category? GetById(int categoryId)
        {
            Category? category = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT CategoryId, CategoryName FROM Categories WHERE CategoryId = @CategoryId", connection);
                command.Parameters.AddWithValue("@CategoryId", categoryId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        category = new Category
                        {
                            CategoryId = (int)reader["CategoryId"],
                            CategoryName = (string)reader["CategoryName"]
                        };
                    }
                }
            }
            return category;
        }

        // READ (Get all)
        public List<Category> GetAll()
        {
            var categories = new List<Category>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT CategoryId, CategoryName FROM Categories", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            CategoryId = (int)reader["CategoryId"],
                            CategoryName = (string)reader["CategoryName"]
                        });
                    }
                }
            }
            return categories;
        }

        // UPDATE
        public void Update(Category category)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "UPDATE Categories SET CategoryName = @CategoryName WHERE CategoryId = @CategoryId";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                command.Parameters.AddWithValue("@CategoryId", category.CategoryId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // DELETE
        public void Delete(int categoryId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "DELETE FROM Categories WHERE CategoryId = @CategoryId";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@CategoryId", categoryId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}