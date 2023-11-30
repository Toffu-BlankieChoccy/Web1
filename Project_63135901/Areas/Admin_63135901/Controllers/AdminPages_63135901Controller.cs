using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using Project_63135901.Models;

namespace Project_63135901.Areas.Admin_63135901.Controllers
{
    [Area("Admin_63135901")]
    public class AdminPages_63135901Controller : Controller
    {
        private readonly PROJECT_63135901Context _context;

        public AdminPages_63135901Controller(PROJECT_63135901Context context)
        {
            _context = context;
        }

        // GET: Admin_63135901/AdminPages_63135901
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10; //Show 10 rows every page
            var lsPage = _context.Pages
                .AsNoTracking()
                .OrderByDescending(x => x.PagesId);
            PagedList<Page> models = new PagedList<Page>(lsPage, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin_63135901/AdminPages_63135901/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pages == null)
            {
                return NotFound();
            }

            var page = await _context.Pages
                .FirstOrDefaultAsync(m => m.PagesId == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // GET: Admin_63135901/AdminPages_63135901/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin_63135901/AdminPages_63135901/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PagesId,PageName,Contents,Thumb,Published,Title,MetaDesc,MetaKey,Alias,CreatedDate,Ordering")] Page page)
        {
            if (ModelState.IsValid)
            {
                _context.Add(page);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(page);
        }

        // GET: Admin_63135901/AdminPages_63135901/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pages == null)
            {
                return NotFound();
            }

            var page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        // POST: Admin_63135901/AdminPages_63135901/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PagesId,PageName,Contents,Thumb,Published,Title,MetaDesc,MetaKey,Alias,CreatedDate,Ordering")] Page page)
        {
            if (id != page.PagesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(page);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(page.PagesId))
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
            return View(page);
        }

        // GET: Admin_63135901/AdminPages_63135901/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pages == null)
            {
                return NotFound();
            }

            var page = await _context.Pages
                .FirstOrDefaultAsync(m => m.PagesId == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // POST: Admin_63135901/AdminPages_63135901/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pages == null)
            {
                return Problem("Entity set 'PROJECT_63135901Context.Pages'  is null.");
            }
            var page = await _context.Pages.FindAsync(id);
            if (page != null)
            {
                _context.Pages.Remove(page);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PageExists(int id)
        {
          return (_context.Pages?.Any(e => e.PagesId == id)).GetValueOrDefault();
        }
    }
}
