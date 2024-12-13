using System.Collections.Generic;

namespace Urban_Tide_Bus_Booking_Service_Application.Models
{
    // User model class
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
        public string ContactNumber { get; set; }

        public User()
        {
            Roles = new List<string>();
        }
    }
}