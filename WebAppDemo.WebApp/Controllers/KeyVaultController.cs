using Microsoft.AspNetCore.Mvc;

namespace WebAppDemo.WebApp.Controllers
{
	public class KeyVaultController : Controller
	{
		private readonly IConfiguration _configuration;

		public KeyVaultController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public IActionResult Index()
		{
	

			// Fetch secret from Azure Key Vault
			var secretValue1 = _configuration["Secret1"];
			var secretValue2 = _configuration["Secret2"];

			// Pass data to the view
			ViewBag.SecretValue1 = secretValue1;
			ViewBag.SecretValue2 = secretValue2;

			return View();
		}
	}
}
