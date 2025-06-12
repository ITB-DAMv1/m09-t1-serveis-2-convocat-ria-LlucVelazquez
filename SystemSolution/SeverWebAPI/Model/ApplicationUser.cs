using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ServerWebAPI.Model
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public override string Email { get; set; }

        [Range(1, 1000)]
        public int NumEmployees { get; set; }

        public bool IsVip { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }
    }
}
