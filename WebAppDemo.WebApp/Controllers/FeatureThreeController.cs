using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

namespace WebAppDemo.WebApp.Controllers
{
    [FeatureGate("FeatureThree")]
    public class FeatureThreeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
