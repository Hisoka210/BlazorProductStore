using System.Data.SqlClient;
using BlazorProductStore.DAL.Models;

namespace BlazorProductStore.DAL
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User? FindByGoogleId(string googleId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT Id, GoogleId, Email, Name, CreatedAt FROM Users WHERE GoogleId = @GoogleId";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@GoogleId", googleId);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = (int)reader["Id"],
                            GoogleId = (string)reader["GoogleId"],
                            Email = (string)reader["Email"],
                            Name = (string)reader["Name"],
                            CreatedAt = (DateTime)reader["CreatedAt"]
                        };
                    }
                }
            }
            return null;
        }

        public User Add(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                // The OUTPUT clause gets the newly created Id and CreatedAt values back
                var sql = @"INSERT INTO Users (GoogleId, Email, Name) 
                            OUTPUT INSERTED.Id, INSERTED.CreatedAt
                            VALUES (@GoogleId, @Email, @Name)";

                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@GoogleId", user.GoogleId);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Name", user.Name);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user.Id = (int)reader["Id"];
                        user.CreatedAt = (DateTime)reader["CreatedAt"];
                    }
                }
            }
            return user;
        }
    }
}