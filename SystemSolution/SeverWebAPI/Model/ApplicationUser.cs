using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ServerWebAPI.Model
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(1, 1000)]
        public int NumEmployees { get; set; }

        public bool IsVip { get; set; }

        [Required]
        public DateTime DateRegister { get; set; }
    }
}
