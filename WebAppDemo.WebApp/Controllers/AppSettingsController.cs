using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebAppDemo.WebApp.Controllers
{
	public class AppSettingsController : Controller
	{
        private readonly IOptionsSnapshot<Settings> _settings;

        public AppSettingsController(IOptionsSnapshot<Settings> settings)
		{
            _settings = settings;
        }

        public IActionResult Index()
        {
            return View(_settings.Value);
        }
    }
	public class Settings
	{
		public string One { get; set; }
		public string Two { get; set; }
		public string Three { get; set; }
		public string Four { get; set; }
	}
}
