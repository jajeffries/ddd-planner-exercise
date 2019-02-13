using Newtonsoft.Json;

namespace Planner
{
    public class User
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsLoggedIn = false;
        public readonly Role Role;

        public User(Role role) : this(string.Empty, string.Empty, string.Empty, role, false)
        {
        }

        public User(string username, Role role) : this(string.Empty, string.Empty, username, role, false)
        {
        }

        public User(string firstName, string lastName, string username, Role role) : this(firstName, lastName, username, role, false)
        {
        }

        [JsonConstructor]
        public User(string firstName, string lastName, string username, Role role, bool isLoggedIn)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Role = role;
            IsLoggedIn = isLoggedIn;
        }

        public bool IsAuthenticated()
        {
            return this.IsLoggedIn;
        }

        public void Login()
        {
            this.IsLoggedIn = true;
        }

        public bool HasPermission(Permission permission)
        {
            return this.Role.AllowedTo(permission);
        }
    }
}