using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Urban_Tide_Bus_Booking_Service_Application.Models;
using Urban_Tide_Bus_Booking_Service_Application.Repositories;

namespace Urban_Tide_Bus_Booking_Service_Application.Manager
{
    public class UserManager
    {
        private static readonly UserManager _instance = new UserManager();
        private readonly IUserRepository _userRepository;

        private UserManager()
        {
            var repositoryType = GetRepositoryTypeFromConfig(); // Fetch repository type
            _userRepository = CreateUserRepository(repositoryType) ?? throw new InvalidOperationException("Failed to initialize user repository.");
        }

        public static UserManager Instance => _instance;

        private IUserRepository CreateUserRepository(string repositoryType)
        {
            switch (repositoryType)
            {
                case "Access":
                    string accessConnectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Path.Combine(Application.StartupPath, "UsersDB.msacc.accdb")}";
                    return new AccessUserRepository(accessConnectionString);

                case "Fake":
                    return new FakeUserRepository();

                case "Json":
                    string jsonFilePath = Path.Combine(Application.StartupPath, "User.json");
                    return new JsonUserRepository(jsonFilePath);

                default:
                    throw new NotSupportedException($"Repository type '{repositoryType}' is not supported.");
            }
        }

        private string GetRepositoryTypeFromConfig()
        {
            try
            {
                string jsonPath = Path.Combine(Application.StartupPath, "appsettings.json");
                string jsonString = File.ReadAllText(jsonPath);

                var config = JsonConvert.DeserializeObject<AppSettings>(jsonString);
                return config?.Mode ?? "Fake"; // Default to "Fake"
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading configuration: {ex.Message}");
                return "Fake"; // Default to "Fake"
            }
        }

        public List<User> GetAllUsers() => _userRepository.GetUsers();

        public void AddNewUser(string username, string password, List<string> roles)
        {
            var user = new User
            {
                Username = username,
                Password = password,
                Roles = roles
            };

            _userRepository.AddUser(user);
        }

        public User AuthenticateUser(string username, string password)
        {
            var isValid = _userRepository.ValidateUser(username, password);
            if (!isValid) return null;

            return _userRepository.GetUsers().FirstOrDefault(i => i.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }

    public class AppSettings
    {
        public string Mode { get; set; }
    }
}
