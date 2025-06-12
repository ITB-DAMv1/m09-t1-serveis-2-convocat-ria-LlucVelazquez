using System.ComponentModel.DataAnnotations;

namespace ServerWebAPI.DTOs
{
	public class UserDTO
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public int NumEmployees { get; set; }
		public bool IsVip { get; set; }
		public DateTime DateRegister { get; set; }
	}
}
