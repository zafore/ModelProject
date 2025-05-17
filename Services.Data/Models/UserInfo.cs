using System;
using System.Collections.Generic;

namespace Services.Data.Models
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserFullName { get; set; } = null!;
        public string PassWord { get; set; } = null!;
        public bool? Isactive { get; set; }
        public DateTime? CrDate { get; set; }
        public int? CrUserId { get; set; }
        public DateTime? UpDate { get; set; }
        public int? UpUserId { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
