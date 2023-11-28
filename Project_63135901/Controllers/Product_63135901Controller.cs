using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_63135901.Models;

namespace Project_63135901.Controllers
{
	public class Product_63135901Controller : Controller
	{
		private readonly PROJECT_63135901Context _context;

		public Product_63135901Controller(PROJECT_63135901Context context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Details(int id)
        {
			var product = _context.Products.Include(x=>x.Cat).FirstOrDefault(x=>x.ProductId == id);
			if (product == null) {
				return RedirectToAction("Index");
			}
            return View(product);
        }
    }
}
