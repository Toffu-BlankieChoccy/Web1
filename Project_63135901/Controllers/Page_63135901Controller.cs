using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_63135901.Models;

namespace Project_63135901.Controllers
{
    
    public class Page_63135901Controller : Controller
    {
        private readonly PROJECT_63135901Context _context;
        public Page_63135901Controller(PROJECT_63135901Context context)
        {
            _context = context;
        }

        //GET: page/Alias
        [Route("/page/{Alias}", Name = "PagesDetail")]
        public IActionResult Details(string Alias)
        {
            if(string.IsNullOrEmpty(Alias)) { return RedirectToAction("Index", "Home_63135901"); }
            var page = _context.Pages.AsNoTracking().SingleOrDefault(x => x.Alias == Alias);
            if(page == null)
            {
                return RedirectToAction("Index", "Home_63135901");
            }
            return View(page);
        }
    }
}
