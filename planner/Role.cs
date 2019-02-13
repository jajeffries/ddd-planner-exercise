using System.Collections.Generic;
using System.Linq;

namespace Planner
{
    public class Role
    {
        private readonly IEnumerable<Permission> permissions;

        public Role(IEnumerable<Permission> permissions)
        {
            this.permissions = permissions;
        }

        public bool AllowedTo(Permission permission)
        {
            return this.permissions.Any(p => p == permission);
        }
    }
}