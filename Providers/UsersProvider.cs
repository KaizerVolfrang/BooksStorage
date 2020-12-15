using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BooksProject.Models;

namespace BooksProject.Providers
{
    public class UsersProvider
    {
        private SqlConnection _connection;

        public UsersProvider(SqlConnection connection)
        {
            _connection = connection;
        }

        public User Get(string login)
        {
            var result = new User();

            try
            {
                _connection.Open();
                var command = new SqlCommand(@"
                SELECT * FROM
                    [Users]
                WHERE
                    [Login] = @Login
                ",
                    _connection);
                command.Parameters.AddWithValue("@Login", login);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User()
                        {
                            Id = reader.GetInt32(0),
                            Login = reader.GetString(1),
                            Password = reader.GetString(2),
                            Role = reader.GetString(3)
                        };
                        result = user;
                    }
                }
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }
    }
}
