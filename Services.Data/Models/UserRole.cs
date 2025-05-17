using System;
using System.Collections.Generic;

namespace Services.Data.Models
{
    public partial class UserRole
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime? CrDate { get; set; }
        public int? CrUser { get; set; }
        public DateTime? UpDate { get; set; }
        public int? UpuserId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual UserInfo User { get; set; } = null!;
    }
}
