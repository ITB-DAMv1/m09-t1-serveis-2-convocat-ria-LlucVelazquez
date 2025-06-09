using Microsoft.AspNetCore.Identity;

namespace ServerWebAPI.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public int NumEmployees { get; set; }
        public bool IsVip { get; set; }
    }
}
