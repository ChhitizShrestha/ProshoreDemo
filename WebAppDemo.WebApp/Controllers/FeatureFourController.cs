using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

namespace WebAppDemo.WebApp.Controllers
{
    [FeatureGate("FeatureFour")]
    public class FeatureFourController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
