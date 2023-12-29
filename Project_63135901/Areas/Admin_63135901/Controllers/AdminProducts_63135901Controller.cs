using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using Project_63135901.Extensions;
using Project_63135901.Helper;
using Project_63135901.Models;

namespace Project_63135901.Areas.Admin_63135901.Controllers
{
	[Area("Admin_63135901")]
	public class AdminProducts_63135901Controller : Controller
	{
		private readonly PROJECT_63135901Context _context;
		public INotyfService _notyfService { get; }
		public AdminProducts_63135901Controller(PROJECT_63135901Context context, INotyfService notyfService)
		{
			_context = context;
			_notyfService = notyfService;
		}

		// GET: Admin_63135901/AdminProducts_63135901
		public IActionResult Index(int page = 1, int CatID = 0)
		{
			var pageNumber = page;
			var pageSize = 10; //Show 10 rows every page
			List<Product> lsProduct = new List<Product>();
			if (CatID != 0)
			{
				lsProduct = _context.Products
			   .AsNoTracking()
			   .Where(x => x.CatId == CatID)
			   .Include(x => x.Cat)
			   .OrderByDescending(x => x.ProductId).ToList();
			}
			else
			{
				lsProduct = _context.Products
							   .AsNoTracking()
							   .Include(x => x.Cat)
							   .OrderByDescending(x => x.CatId).ToList();
			}

			PagedList<Product> models = new PagedList<Product>(lsProduct.AsQueryable(), pageNumber, pageSize);
			ViewBag.CurrentCatID = CatID;
			ViewBag.CurrentPage = pageNumber;
			ViewData["Product"] = new SelectList(_context.Categories, "CatId", "CatName", CatID);
			return View(models);
		}

		public IActionResult Filter(int CatID = 0)
		{
			var url = $"/Admin_63135901/AdminProducts_63135901?CatID={CatID}";
			if (CatID == 0)
			{
				url = $"/Admin_63135901/AdminProducts_63135901";
			}
			return Json(new
			{
				status = "success",
				redirectUrl = url
			});
		}

		// GET: Admin_63135901/AdminProducts_63135901/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Products == null)
			{
				return NotFound();
			}

			var product = await _context.Products
				.Include(p => p.Cat)
				.FirstOrDefaultAsync(m => m.ProductId == id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}

		// GET: Admin_63135901/AdminProducts_63135901/Create
		public IActionResult Create()
		{
			ViewData["Product"] = new SelectList(_context.Categories, "CatId", "CatName");
			return View();
		}

		// POST: Admin_63135901/AdminProducts_63135901/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ProductId,ProductName,ShortDescription,ProductDescription,CatId,Price,Discount,Thumb,Video,DateCreated,DateModified,BestSellers,HomeFlag,Active,Tags,Title,Alias,MetaDesc,MetaKey,UnitsInStock")] Product product, Microsoft.AspNetCore.Http.IFormFile fThumb)
		{
			if (ModelState.IsValid)
			{
				product.ProductName = Extension.ToTitleCase(product.ProductName);
				if (fThumb != null)
				{
					string extension = Path.GetExtension(fThumb.FileName);
					string image = Extension.ToUrlFriendly(product.ProductName) + extension;
					product.Thumb = await Utilities.UploadFile(fThumb, @"products", image.ToLower());
				}
				if (string.IsNullOrEmpty(product.Thumb))
				{
					product.Thumb = "default.jpg";
				}
				product.Alias = Extension.ToUrlFriendly(product.ProductName);
				product.DateModified = DateTime.Now;
				product.DateCreated = DateTime.Now;

				_context.Add(product);
				await _context.SaveChangesAsync();
				_notyfService.Success("Thêm sản phẩm thành công");
				return RedirectToAction(nameof(Index));
			}
			ViewData["Product"] = new SelectList(_context.Categories, "CatId", "CatName", product.CatId);
			return View(product);
		}

		// GET: Admin_63135901/AdminProducts_63135901/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Products == null)
			{
				return NotFound();
			}

			var product = await _context.Products.FindAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			ViewData["Product"] = new SelectList(_context.Categories, "CatId", "CatName", product.CatId);
			return View(product);
		}

		// POST: Admin_63135901/AdminProducts_63135901/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ShortDescription,ProductDescription,CatId,Price,Discount,Thumb,Video,DateCreated,DateModified,BestSellers,HomeFlag,Active,Tags,Title,Alias,MetaDesc,MetaKey,UnitsInStock")] Product product, Microsoft.AspNetCore.Http.IFormFile fThumb)
		{
			if (id != product.ProductId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					product.ProductName = Extension.ToTitleCase(product.ProductName);
					if (fThumb != null)
					{
						string extension = Path.GetExtension(fThumb.FileName);
						string image = Extension.ToUrlFriendly(product.ProductName) + extension;
						product.Thumb = await Utilities.UploadFile(fThumb, @"products", image.ToLower());
					}
					if (string.IsNullOrEmpty(product.Thumb)) product.Thumb = "default.jpg";
					product.Alias = Extension.ToUrlFriendly(product.ProductName);
					product.DateModified = DateTime.Now;

					_context.Update(product);
					_notyfService.Success("Lưu thành công");
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ProductExists(product.ProductId))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			ViewData["Product"] = new SelectList(_context.Categories, "CatId", "CatName", product.CatId);
			return View(product);
		}

		// GET: Admin_63135901/AdminProducts_63135901/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Products == null)
			{
				return NotFound();
			}

			var product = await _context.Products
				.Include(p => p.Cat)
				.FirstOrDefaultAsync(m => m.ProductId == id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// POST: Admin_63135901/AdminProducts_63135901/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Products == null)
			{
				return Problem("Entity set 'PROJECT_63135901Context.Products'  is null.");
			}
			var product = await _context.Products.FindAsync(id);
			if (product != null)
			{
				_context.Products.Remove(product);
			}

			await _context.SaveChangesAsync();
			_notyfService.Success("Xóa thành công");
			return RedirectToAction(nameof(Index));
		}

		private bool ProductExists(int id)
		{
			return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
		}
	}
}