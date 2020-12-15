using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BooksProject.Providers
{
    public class StorageContext
    {
        public UsersProvider usersProvider { get; private set; }
        public AutorisationService AutorisationService { get; private set; }
        public BooksProvider booksProvider { get; private set; }

        public StorageContext()
        {
            var connection = CreateConnection();
            usersProvider = new UsersProvider(connection);
            AutorisationService = new AutorisationService(usersProvider);
            booksProvider = new BooksProvider(connection);
        }

        public SqlConnection CreateConnection()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = @"DESKTOP-MCFVAK1\SQLEXPRESS",
                InitialCatalog = "BooksStorage",
                IntegratedSecurity = true
            };
            var str = builder.ToString();
            return new SqlConnection(str);
        }
    }
}
