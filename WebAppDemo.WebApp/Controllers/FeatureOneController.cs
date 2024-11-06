using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

namespace WebAppDemo.WebApp.Controllers
{
    [FeatureGate("FeatureOne")]
    public class FeatureOneController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
