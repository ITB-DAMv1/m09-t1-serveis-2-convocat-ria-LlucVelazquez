using ClientWebRP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientWebRP.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger _logger;
        [BindProperty]
        public RegisterDTO Register { get; set; } = new();
        public string? ErrorMessage { get; set; }
        public RegisterModel(IHttpClientFactory httpClient, ILogger<RegisterModel> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var client = _httpClient.CreateClient("ServerApi");
            var response = await client.PostAsJsonAsync("api/Auth/register", Register);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Login");
            }
            else
            {
                _logger.LogInformation("Register failed");
                ErrorMessage = "Invalid register attempt.";
                return Page();
            }
        }
    }
}
