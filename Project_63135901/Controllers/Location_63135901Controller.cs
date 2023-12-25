using Microsoft.AspNetCore.Mvc;
using Project_63135901.Models;

namespace Project_63135901.Controllers
{
    public class Location_63135901Controller : Controller
    {
        private readonly PROJECT_63135901Context _context;
        public Location_63135901Controller(PROJECT_63135901Context context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View();
        }
        //GET LOCATION
        public ActionResult QuanHuyenList(int LocationId)
        {
            var QuanHuyens = _context.Locations
                .OrderBy(x => x.LocationId)
                .Where(x => x.ParentCode == LocationId && x.Levels == 2)
                .OrderBy(x => x.LocationName)
                .ToList();
            return Json(QuanHuyens);
        }

        public ActionResult PhuongXaList(int LocationId)
        {
            var PhuongXas = _context.Locations
                .OrderBy(x => x.LocationId)
                .Where(x => x.ParentCode == LocationId && x.Levels == 3)
                .OrderBy(x => x.LocationName)
                .ToList();
            return Json(PhuongXas);
        }
    }
}
