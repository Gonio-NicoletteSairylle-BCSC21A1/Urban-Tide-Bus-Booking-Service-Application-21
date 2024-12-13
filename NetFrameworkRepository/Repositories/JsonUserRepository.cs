using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

using Urban_Tide_Bus_Booking_Service_Application.Models;

namespace Urban_Tide_Bus_Booking_Service_Application.Repositories
{
    public class JsonUserRepository : IUserRepository
    {
        private readonly string _filePath;

        public JsonUserRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<User> GetUsers()
        {
            if (!File.Exists(_filePath))
                return new List<User>();

            string jsonContent = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<User>>(jsonContent) ?? new List<User>();
        }

        public void AddUser(User user)
        {
            var users = GetUsers();
            users.Add(user);

            string jsonContent = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(_filePath, jsonContent);
        }

        public bool ValidateUser(string username, string password)
        {
            var users = GetUsers();
            return users.Exists(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.Password == password);
        }
    }
}