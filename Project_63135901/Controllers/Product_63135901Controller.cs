using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
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

        [Route("shop.html", Name = "ShopProduct")]
        public IActionResult Index(int? page)
		{
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 10; //Show 10 rows every page
                var lsProducts = _context.Products
                   .AsNoTracking()
                   .OrderByDescending(x => x.DateCreated);
                PagedList<Product> models = new PagedList<Product>(lsProducts, pageNumber, pageSize);
                ViewBag.CurrentPage = pageNumber;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home_63135901");
            }


        }

        [Route("/{Alias}--{id}", Name = "ListProduct")]
        public IActionResult List(int id, int page = 1)
		{
            try
            {
                var pageSize = 10; //Show 10 rows every page
                var danhmuc = _context.Categories.Find(id);
                var lsProducts = _context.Products
                   .AsNoTracking()
                   .Where(x => x.CatId == id)
                   .OrderByDescending(x => x.DateCreated);
                PagedList<Product> models = new PagedList<Product>(lsProducts, page, pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.CurrentCat = danhmuc;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index","Home_63135901");
            }
            
		}

        [Route("/{Alias}--{id}.html", Name = "ProductDetails")]
        public IActionResult Details(int id)
        {
            try
            {
                var product = _context.Products.Include(x => x.Cat).FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch
            {
                return RedirectToAction("Index", "Home_63135901");
            }

        }
    }
}
