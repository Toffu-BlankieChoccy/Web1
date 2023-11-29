using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_63135901.Models;

namespace Project_63135901.Areas.Admin_63135901.Controllers
{
    [Area("Admin_63135901")]
    public class AdminAccounts_63135901Controller : Controller
    {
        private readonly PROJECT_63135901Context _context;

        public AdminAccounts_63135901Controller(PROJECT_63135901Context context)
        {
            _context = context;
        }

        // GET: Admin_63135901/AdminAccounts_63135901
        public async Task<IActionResult> Index()
        {
            ViewData["Role"] = new SelectList(_context.Roles, "RoleId", "RoleName");

            List<SelectListItem> lsStatus = new List<SelectListItem>();
            lsStatus.Add(new SelectListItem() { Text = "Online", Value = "1" });
            lsStatus.Add(new SelectListItem() { Text = "Offline", Value = "0" });
            ViewData["Status"] = lsStatus;

            var pROJECT_63135901Context = _context.Accounts.Include(a => a.Role);
            return View(await pROJECT_63135901Context.ToListAsync());
        }

        // GET: Admin_63135901/AdminAccounts_63135901/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Admin_63135901/AdminAccounts_63135901/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId");
            return View();
        }

        // POST: Admin_63135901/AdminAccounts_63135901/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,Phone,Email,AccPassword,Salt,Active,Fullname,RoleId,LastLogin,CreateDate")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", account.RoleId);
            return View(account);
        }

        // GET: Admin_63135901/AdminAccounts_63135901/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", account.RoleId);
            return View(account);
        }

        // POST: Admin_63135901/AdminAccounts_63135901/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,Phone,Email,AccPassword,Salt,Active,Fullname,RoleId,LastLogin,CreateDate")] Account account)
        {
            if (id != account.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", account.RoleId);
            return View(account);
        }

        // GET: Admin_63135901/AdminAccounts_63135901/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Admin_63135901/AdminAccounts_63135901/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'PROJECT_63135901Context.Accounts'  is null.");
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
          return (_context.Accounts?.Any(e => e.AccountId == id)).GetValueOrDefault();
        }
    }
}
