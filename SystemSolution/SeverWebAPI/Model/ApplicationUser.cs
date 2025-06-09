using Microsoft.AspNetCore.Identity;

namespace SeverWebAPI.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
