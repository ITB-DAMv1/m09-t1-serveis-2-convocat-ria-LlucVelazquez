using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerWebAPI.Data;
using ServerWebAPI.DTOs;
using ServerWebAPI.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
		private readonly AppDbContext _context;
		public AuthController(UserManager<ApplicationUser> userManager, ILogger<AuthController> logger, IConfiguration configuration, AppDbContext context)
		{
			_userManager = userManager;
			_logger = logger;
			_configuration = configuration;
			_context = context;
		}
		[HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name,
                NumEmployees = model.NumEmployees,
                IsVip = model.IsVip,
				DateRegister = DateTime.Now
			};
            if (model.Password != model.PasswordConfirmed)
            {
                return BadRequest("Passwords do not match");
            }
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
                return Ok("Usuari registrat");

            return BadRequest(result.Errors);
        }
        [HttpGet("getUser/{id}")]
		public async Task<IActionResult> GetUser(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound("User not found");
			}
			var userDTO = new UserDTO
			{
				Id = user.Id,
				UserName = user.UserName,
				Email = user.Email,
				Name = user.Name,
				NumEmployees = user.NumEmployees,
				IsVip = user.IsVip,
				DateRegister = user.DateRegister
			};
			return Ok(userDTO);
		}
		[Authorize]
		[HttpGet("Profile")]
		public async Task<ActionResult<UserDTO>> GetUserProfile()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == userId);
			var userDTO = new UserDTO
			{
				Id = user.Id,
				UserName = user.UserName,
				Email = user.Email,
				Name = user.Name,
				NumEmployees = user.NumEmployees,
				IsVip = user.IsVip,
				DateRegister = user.DateRegister
			};
			return Ok(userDTO);
		}
		[HttpGet("getAllUsers")]
		public async Task<IActionResult> GetAllUsers()
		{
			var users = _userManager.Users.ToList();
			var userDTOs = users.Select(user => new UserDTO
			{
				Id = user.Id,
				UserName = user.UserName,
				Email = user.Email,
				Name = user.Name,
				NumEmployees = user.NumEmployees,
				IsVip = user.IsVip,
				DateRegister = user.DateRegister
			}).ToList();
			return Ok(userDTOs);
		}
        [HttpPut("updateUser")]
		public async Task<IActionResult> UpdateUser([FromBody] UserDTO model)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == userId);
			if (user == null)
			{
				return NotFound("User not found");
			}
			user.UserName = model.UserName;
			user.Email = model.Email;
			user.Name = model.Name;
			user.NumEmployees = model.NumEmployees;
			user.IsVip = model.IsVip;

			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
				return Ok("User updated successfully");
			return BadRequest(result.Errors);
		}
		[HttpPost("admin/register")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDTO model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name,
                NumEmployees = model.NumEmployees,
                IsVip = model.IsVip,
				DateRegister = DateTime.Now
			};
            if (model.Password != model.PasswordConfirmed)
            {
                return BadRequest("Passwords do not match");
            }
            var result = await _userManager.CreateAsync(user, model.Password);
            var roleResult = new IdentityResult();
            if (result.Succeeded)
            {
                roleResult = await _userManager.AddToRoleAsync(user, "Admin");
            }
            if (result.Succeeded && roleResult.Succeeded)
            {
                return Ok("Usuari registrat");
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized("Invalid credentials");
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            var roles = await _userManager.GetRolesAsync(user);
            if (roles != null && roles.Count > 0)
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }
            return Ok(CreateToken(claims.ToArray()));
        }
        [Authorize(Roles = "Admin, PR")]
		[HttpDelete("deleteUser/{id}")]
		public async Task<IActionResult> DeleteUser(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound("User not found");
			}
			var result = await _userManager.DeleteAsync(user);
			if (result.Succeeded)
				return Ok("User deleted successfully");
			return BadRequest(result.Errors);
		}
		private string CreateToken(Claim[] claims)
        {
            var jwtConfig = _configuration.GetSection("JwtSettings");
            var secretKey = jwtConfig["Key"];
            var issuer = jwtConfig["Issuer"];
            var audience = jwtConfig["Audience"];
            var expirationMinutes = int.Parse(jwtConfig["ExpirationMinutes"]);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expirationMinutes),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}