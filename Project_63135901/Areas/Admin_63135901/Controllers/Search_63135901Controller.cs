using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_63135901.Models;

namespace Project_63135901.Areas.Admin_63135901.Controllers
{
    [Area("Admin_63135901")]
    public class Search_63135901Controller : Controller
    {
        private readonly PROJECT_63135901Context _context;
        public Search_63135901Controller (PROJECT_63135901Context context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult FindProduct(string keyword)
        {
            List<Product> ls = new List<Product>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListProductsSearchPartial", null);
            }
            
            ls = _context.Products.AsNoTracking()
                                  .Include(a => a.Cat)
                                  .Where(x => x.ProductName.Contains(keyword))
                                  .OrderByDescending(x => x.ProductName)
                                  .Take(10)
                                  .ToList();
            if (ls == null)
            {
                return PartialView("ListProductsSearchPartial", null);
            }
            else
            {
                return PartialView("ListProductsSearchPartial", ls);
            }
        }
    }
}

