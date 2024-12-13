using System.Collections.Generic;
using Urban_Tide_Bus_Booking_Service_Application.Models;

namespace Urban_Tide_Bus_Booking_Service_Application.Repositories
{


    public interface IUserRepository
    {
        List<User> GetUsers();
        void AddUser(User user);
        bool ValidateUser(string username, string password);
    }

}