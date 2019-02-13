using System.Collections.Generic;
using System.Linq;

namespace Planner
{
    public class Role
    {
        public readonly IEnumerable<Permission> Permissions;

        public Role(IEnumerable<Permission> permissions)
        {
            this.Permissions = permissions;
        }

        public bool AllowedTo(Permission permission)
        {
            return this.Permissions.Any(p => p == permission);
        }
    }
}