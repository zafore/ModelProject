using System;
using System.Collections.Generic;

namespace Services.Data.Models
{
    public partial class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public bool RoleIsactive { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
