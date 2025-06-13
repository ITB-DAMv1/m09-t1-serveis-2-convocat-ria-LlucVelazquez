using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientWebRP.Pages
{
    public class DeleteModel : PageModel
    {
		private readonly ILogger<DeleteModel> _logger;
		private readonly IHttpClientFactory _httpClientFactory;
		[BindProperty]
		public string UserId { get; set; } = string.Empty;
		public string? ErrorMessage { get; set; }
		public DeleteModel(IHttpClientFactory httpClientFactory, ILogger<DeleteModel> logger)
		{
			_httpClientFactory = httpClientFactory;
			_logger = logger;
		}
		public async Task<IActionResult> OnPostAsync()
		{
			if (string.IsNullOrEmpty(UserId))
			{
				ErrorMessage = "User ID cannot be empty.";
				return Page();
			}
			try
			{
				var client = _httpClientFactory.CreateClient("ServerApi");
				var response = await client.DeleteAsync($"api/auth/deleteUser/{UserId}");
				if (response.IsSuccessStatusCode)
				{
					_logger.LogInformation("User deleted successfully.");
					return RedirectToPage("/Index");
				}
				else
				{
					ErrorMessage = "Error deleting user.";
					_logger.LogError("Error deleting user: {0}", response.ReasonPhrase);
					return Page();
				}
			}
			catch (Exception ex)
			{
				ErrorMessage = "An error occurred while deleting the user.";
				_logger.LogError(ex, "Error in OnPostAsync");
				return Page();
			}
		}
		public void OnGet()
        {
        }
    }
}
