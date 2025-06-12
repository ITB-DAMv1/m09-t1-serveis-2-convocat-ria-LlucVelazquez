using System.ComponentModel.DataAnnotations;

namespace ClientWebRP.Model
{
    public class RegisterDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public int NumEmployees { get; set; } = 0;
        [Required]
        public bool isVip { get; set; } = false;
        [Required]
        public DateTime DateRegister { get; set; } = DateTime.Now;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string PasswordConfirmed { get; set; } = string.Empty;
    }
}
