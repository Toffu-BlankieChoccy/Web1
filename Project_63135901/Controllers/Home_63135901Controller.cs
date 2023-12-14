using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_63135901.Models;
using Project_63135901.ModelViews;
using System.Collections.Generic;
using System.Diagnostics;

namespace Project_63135901.Controllers
{
	public class Home_63135901Controller : Controller
	{
		private readonly ILogger<Home_63135901Controller> _logger;
		private readonly PROJECT_63135901Context _context;

		public Home_63135901Controller(ILogger<Home_63135901Controller> logger, PROJECT_63135901Context context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
            HomeViewVM model = new HomeViewVM();

            var lsProducts = _context.Products.AsNoTracking()
                                .Where(x => x.Active == true && x.HomeFlag == true)
                                .OrderByDescending(x => x.DateCreated)
                                .ToList();

            List<ProductHomeVM> lsProductViews = new List<ProductHomeVM>();

            var lsCats = _context.Categories
								.AsNoTracking()
                                .Where(x => x.Publish == true /*&& x.ParentId == 0*/) /*Thêm để có lại danh mục*/
                                .OrderByDescending(x => x.Ordering)
                                .ToList();

            foreach (var item in lsCats)
            {
                ProductHomeVM productHome = new ProductHomeVM();
                productHome.category = item;
                productHome.lsProducts = lsProducts.Where(x => x.CatId == item.CatId).ToList();
                lsProductViews.Add(productHome);
            }

            model.Products = lsProductViews;
            ViewBag.AllProducts = lsProducts;
            return View(model);

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