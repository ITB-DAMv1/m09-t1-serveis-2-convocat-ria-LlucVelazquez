using System.ComponentModel.DataAnnotations;

namespace ServerWebAPI.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string PasswordConfirmed { get; set; }

        [Range(1, 1000)]
        public int NumEmployees { get; set; }

        public bool IsVip { get; set; }
    }
}
