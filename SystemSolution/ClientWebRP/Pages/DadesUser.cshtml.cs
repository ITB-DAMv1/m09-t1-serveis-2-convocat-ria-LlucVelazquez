using ClientWebRP.Model;
using ClientWebRP.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ClientWebRP.Pages
{
    public class DadesUserModel : PageModel
    {
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILogger<DadesUserModel> _logger;

		public UserDTO? Profile { get; set; }
		public string? ErrorMessage { get; set; }

		public DadesUserModel(IHttpClientFactory httpClient, ILogger<DadesUserModel> logger)
		{
			_httpClientFactory = httpClient;
			_logger = logger;
		}

		public async Task OnGetAsync()
		{
			try
			{
				var client = _httpClientFactory.CreateClient("ServerApi");
				var token = HttpContext.Session.GetString("AuthToken");

				if (TokenHelper.IsTokenSession(token))
				{
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
				}

				var response = await client.GetAsync($"api/auth/Profile");
				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadAsStringAsync();
					Profile = JsonSerializer.Deserialize<UserDTO>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error Loading User Profile");
			}
		}
	}
}
