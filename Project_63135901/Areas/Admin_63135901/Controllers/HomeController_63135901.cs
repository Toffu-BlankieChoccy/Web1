using Microsoft.AspNetCore.Mvc;

namespace Project_63135901.Areas.Admin_63135901.Controllers
{
    public class HomeController_63135901 : Controller
    {
        [Area("Admin_63135901")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
