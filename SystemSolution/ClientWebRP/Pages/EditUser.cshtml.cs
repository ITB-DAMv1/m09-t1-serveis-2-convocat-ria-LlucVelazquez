using ClientWebRP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientWebRP.Pages
{
	public class EditUserModel : PageModel
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILogger<EditUserModel> _logger;
		[BindProperty]
		public UserDTO User { get; set; } = new UserDTO();
		public string? ErrorMessage { get; set; }
		public EditUserModel(IHttpClientFactory httpClientFactory, ILogger<EditUserModel> logger)
		{
			_httpClientFactory = httpClientFactory;
			_logger = logger;
		}
		public void OnGet()
		{
		}
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				ErrorMessage = "Invalid data.";
				return Page();
			}
			try
			{
				var client = _httpClientFactory.CreateClient("ServerApi");
				var token = HttpContext.Session.GetString("AuthToken");
				var response = await client.PutAsJsonAsync($"api/auth/updateUser", User);
				if (response.IsSuccessStatusCode)
				{
					_logger.LogInformation("User updated successfully.");
					return RedirectToPage("/Index");
				}
				else
				{
					ErrorMessage = "Error updating user.";
					_logger.LogError("Error updating user: {0}", response.ReasonPhrase);
					return Page(); // Ensure a return statement here  
				}
			}
			catch (Exception ex)
			{
				ErrorMessage = "An error occurred while updating the user.";
				_logger.LogError(ex, "Error in OnPostAsync");
				return Page(); // Ensure a return statement here  
			}
		}
	}
}
