using Microsoft.AspNetCore.Mvc;
using Project_63135901.Models;
using System.Diagnostics;

namespace Project_63135901.Controllers
{
    public class Home_63135901Controller : Controller
    {
        private readonly ILogger<Home_63135901Controller> _logger;

        public Home_63135901Controller(ILogger<Home_63135901Controller> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("lien-he.html", Name = "Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [Route("gioi-thieu.html", Name = "About")]
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}