using System.Collections.Generic;
using System.Linq;
using Urban_Tide_Bus_Booking_Service_Application.Models;

namespace Urban_Tide_Bus_Booking_Service_Application.Repositories
{
    public class FakeUserRepository : IUserRepository
    {
        private List<User> _users;

        public FakeUserRepository()
        {
             _users = new List<User>
            {
                new User { Username = "admin", Password = "admin123", Roles = new List<string> { "admin" } },
                new User { Username = "user", Password = "1234", Roles = new List<string> { "user" } }

            };
        }
        public List<User> GetUsers()
        {
            return _users;
        }
        
        public bool ValidateUser(string username, string password)
        {
            return _users.Any(u => u.Username == username && u.Password == password);
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }
    }
}