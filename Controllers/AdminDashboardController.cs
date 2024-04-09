using Microsoft.AspNetCore.Mvc;

namespace Eagles_Website.Controllers
{
    public class AdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
