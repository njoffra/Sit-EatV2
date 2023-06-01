using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SitnEatV2.Models;

namespace SitnEatV2.Controllers
{
   
    public class AdminController : Controller
    {
        private readonly AuthDbContext _context;

        public AdminController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            return _context.applicationUsers != null ?
                        View(await _context.applicationUsers.ToListAsync()) :
                        Problem("Entity set 'AuthDbContext.applicationUsers'  is null.");
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.applicationUsers == null)
            {
                return NotFound();
            }

            var scaffold = await _context.applicationUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scaffold == null)
            {
                return NotFound();
            }

            return View(scaffold);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,PhoneNumber")] ApplicationUser scaffold)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scaffold);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scaffold);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.applicationUsers == null)
            {
                return NotFound();
            }

            var scaffold = await _context.applicationUsers.FindAsync(id);
            if (scaffold == null)
            {
                return NotFound();
            }
            return View(scaffold);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FirstName,LastName,Email,PhoneNumber")] ApplicationUser scaffold)
        {
            if (id != scaffold.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scaffold);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!scaffoldExists(scaffold.Id))
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
            return View(scaffold);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.applicationUsers == null)
            {
                return NotFound();
            }

            var scaffold = await _context.applicationUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scaffold == null)
            {
                return NotFound();
            }

            return View(scaffold);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.applicationUsers == null)
            {
                return Problem("Entity set 'AuthDbContext.applicationUsers'  is null.");
            }
            var scaffold = await _context.applicationUsers.FindAsync(id);
            if (scaffold != null)
            {
                _context.applicationUsers.Remove(scaffold);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool scaffoldExists(string id)
        {
            return (_context.applicationUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
