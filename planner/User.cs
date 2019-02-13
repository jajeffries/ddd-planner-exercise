namespace Planner
{
    public class User
    {
        private readonly Role role;
        private bool IsLoggedIn = false;

        public User(Role role)
        {
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