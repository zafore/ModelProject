using Microsoft.AspNetCore.Identity;

namespace Services.Data.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}