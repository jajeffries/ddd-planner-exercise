namespace Planner
{
    public class User
    {
        public string Username { get; set; }
        private readonly Role role;
        private bool IsLoggedIn = false;

        public User(Role role) : this(string.Empty, role)
        {
        }

        public User(string username, Role role)
        {
            Username = username;
            this.role = role;
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
            return this.role.AllowedTo(permission);
        }
    }
}