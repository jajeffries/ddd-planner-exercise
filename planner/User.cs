using Newtonsoft.Json;

namespace Planner
{
    public class User
    {
        public string Username { get; set; }
        public readonly Role Role;
        public bool IsLoggedIn = false;

        public User(Role role) : this(string.Empty, role)
        {
        }

        public User(string username, Role role) : this(username, role, false)
        {
        }

        [JsonConstructor]
        public User(string username, Role role, bool isLoggedIn)
        {
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