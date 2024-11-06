using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

namespace WebAppDemo.WebApp.Controllers
{
    [FeatureGate("FeatureTwo")]
    public class FeatureTwoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
