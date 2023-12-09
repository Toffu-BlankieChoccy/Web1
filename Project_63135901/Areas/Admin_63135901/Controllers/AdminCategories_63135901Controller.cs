using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using Project_63135901.Extensions;
using Project_63135901.Helper;
using Project_63135901.Models;

namespace Project_63135901.Areas.Admin_63135901.Controllers
{
    [Area("Admin_63135901")]
    public class AdminCategories_63135901Controller : Controller
    {
        private readonly PROJECT_63135901Context _context;
        public INotyfService _notyfService { get; }

        public AdminCategories_63135901Controller(PROJECT_63135901Context context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin_63135901/AdminCategories_63135901
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10; //Show 10 rows every page
            var lsPage = _context.Categories
                .AsNoTracking()
                .OrderByDescending(x => x.CatId);
            PagedList<Category> models = new PagedList<Category>(lsPage, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin_63135901/AdminCategories_63135901/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CatId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin_63135901/AdminCategories_63135901/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin_63135901/AdminCategories_63135901/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatId,CatName,CatDescription,ParentId,Levels,Ordering,Publish,Thumb,Title,Alias,MetaDesc,MetaKey,Cover,ShcemaMarkup")] Category category, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string image = Extension.ToUrlFriendly(category.CatName) + extension;
                    category.Thumb = await Utilities.UploadFile(fThumb, @"categories", image.ToLower());
                }
                if (string.IsNullOrEmpty(category.Thumb))
                {
                    category.Thumb = "default.jpg";
                }
                _context.Add(category);
                await _context.SaveChangesAsync();
                _notyfService.Success("Tạo thành công");

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin_63135901/AdminCategories_63135901/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin_63135901/AdminCategories_63135901/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatId,CatName,CatDescription,ParentId,Levels,Ordering,Publish,Thumb,Title,Alias,MetaDesc,MetaKey,Cover,ShcemaMarkup")] Category category, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != category.CatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = Extension.ToUrlFriendly(category.CatName) + extension;
                        category.Thumb = await Utilities.UploadFile(fThumb, @"categories", image.ToLower());
                    }
                    if (string.IsNullOrEmpty(category.Thumb))
                    {
                        category.Thumb = "default.jpg";
                    }
                    _context.Update(category);
                    _notyfService.Success("Cập nhật thành công");

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CatId))
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
            return View(category);
        }

        // GET: Admin_63135901/AdminCategories_63135901/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CatId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin_63135901/AdminCategories_63135901/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'PROJECT_63135901Context.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            _notyfService.Success("Xóa thành công");
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.CatId == id)).GetValueOrDefault();
        }
    }
}
