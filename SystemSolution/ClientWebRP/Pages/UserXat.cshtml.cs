using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientWebRP.Pages
{
    public class UserXatModel : PageModel
    {
		public string? Token { get; set; }

		public void OnGet()
		{
			Token = HttpContext.Session.GetString("AuthToken");
		}
	}
}
