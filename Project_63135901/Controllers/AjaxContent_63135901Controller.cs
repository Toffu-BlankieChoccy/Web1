using Microsoft.AspNetCore.Mvc;

namespace Project_63135901.Controllers
{
    public class AjaxContent_63135901Controller : Controller
    {
        public IActionResult HeaderCart()
        {
            return ViewComponent("HeaderCart");
        }
        public IActionResult NumberCart()
        {
            return ViewComponent("NumberCart");
        }
    }
}
