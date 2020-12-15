using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksProject.Models;
using System.Security.Cryptography;

namespace BooksProject.Providers
{
    public class AutorisationService
    {
        private UsersProvider _usersProvider;
        public User Me { get; private set; } = null;

        public AutorisationService(UsersProvider usersProvider)
        {
            _usersProvider = usersProvider;
        }

        public bool VerifyPassword(User user, string rawPassword)
        {
            if(user == null)
            {
                return false;
            }
            var bytes = Encoding.UTF8.GetBytes(rawPassword);
            var h = new HMACSHA256(bytes);
            var hash = h.ComputeHash(bytes);
            var password = Convert.ToBase64String(hash);

            return user.Password == password;
        }

        public User SignIn(string login, string password)
        {
            var user = _usersProvider.Get(login);
            if (user != null && VerifyPassword(user, password))
            {
                Me = user;
                Me.Password = string.Empty;
            }
            return Me;
        }
    }
}
