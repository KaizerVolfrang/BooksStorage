using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BooksProject.Models;

namespace BooksProject.Providers
{
    public class BooksProvider
    {
        private SqlConnection _connection;

        public BooksProvider(SqlConnection connection)
        {
            _connection = connection;
        }

        public List<Book> GetAll()
        {
            var result = new List<Book>();

            try
            {
                _connection.Open();
                var command = new SqlCommand(@"
                SELECT 
                    [Id], 
                    [Name],
                    [Author],
                    [DateOfPublish]
                FROM
                    [Books]",
                    _connection);    
                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book book = new Book()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Author = reader.GetString(2),
                            DateOfPublish = reader.GetDateTime(3)
                        };
                        result.Add(book);
                    }
                }
            }

            finally
            {
                _connection.Close();
            }

            return result;
        }

        public bool Add(Book book)
        {
            var result = false;
            try
            {
                _connection.Open();
                var command = new SqlCommand(@"
                    INSERT INTO 
                        [Books]
                        ([Name],
                        [Author],
                        [DateOfPublish])
                    VALUES 
                        (@Name,
                        @Author,
                        @DateOfPublish)
                    ",
                    _connection);
                command.Parameters.AddWithValue("@Name", book.Name);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@DateOfPublish", book.DateOfPublish);

                result = command.ExecuteNonQuery() > 0;
            }

            finally
            {
                _connection.Close();
            }
            return result;
        }

        public bool Update(Book book)
        {
            var result = false;
            try
            {
                _connection.Open();
                var command = new SqlCommand(@"
                UPDATE 
                    [Books]
                SET
                    [Name] = @Name,
                    [Author] = @Author,
                    [DateOfPublish] = @DateOfPublish
                WHERE
                    [Id] = @Id
                ",
                    _connection);
                command.Parameters.AddWithValue("@Id", book.Id);
                command.Parameters.AddWithValue("@Name", book.Name);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@DateOfPublish", book.DateOfPublish);

                result = command.ExecuteNonQuery() > 0;
            }
              
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public bool Delete(int Id)
        {
            var result = false;
            try
            {
                _connection.Open();
                var command = new SqlCommand(@"
                DELETE FROM 
                    [Books]
                WHERE
                    [Id] = @Id",
                    _connection);

                command.Parameters.AddWithValue("@Id", Id);
                result = command.ExecuteNonQuery() > 0;
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }
    }
}
