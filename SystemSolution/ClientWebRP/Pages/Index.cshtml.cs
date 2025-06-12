using ClientWebRP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace ClientWebRP.Pages;

public class IndexModel : PageModel
{
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly ILogger<IndexModel> _logger;

	public List<UserDTO> Users { get; set; } = new();

	public IndexModel(IHttpClientFactory httpClientFactory, ILogger<IndexModel> logger)
	{
		_httpClientFactory = httpClientFactory;
		_logger = logger;
	}

	public async Task OnGetAsync()
	{
		try
		{
			var client = _httpClientFactory.CreateClient("ServerApi");
			var response = await client.GetAsync("api/Auth/getAllUsers");

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				Users = JsonSerializer.Deserialize<List<UserDTO>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error loading users");
		}
	}
}
