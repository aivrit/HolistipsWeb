using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Holistips.Data;
using Holistips.Models;
using Microsoft.AspNetCore.Authorization;

namespace Holistips.Controllers
{
    public class TipPackagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipPackagesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: TipPackages
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipPackages.ToListAsync());
        }

        // GET: TipPackages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipPackage = await _context.TipPackages.SingleOrDefaultAsync(m => m.ID == id);
            if (tipPackage == null)
            {
                return NotFound();
            }

            return View(tipPackage);
        }


        // GET: TipPackages/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipPackages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PacakgeTitle,PackageDescription")] TipPackage tipPackage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipPackage);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipPackage);
        }

        // GET: TipPackages/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipPackage = await _context.TipPackages.SingleOrDefaultAsync(m => m.ID == id);
            if (tipPackage == null)
            {
                return NotFound();
            }
            return View(tipPackage);
        }

        // POST: TipPackages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PacakgeTitle,PackageDescription")] TipPackage tipPackage)
        {
            if (id != tipPackage.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipPackage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipPackageExists(tipPackage.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(tipPackage);
        }

        // GET: TipPackages/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipPackage = await _context.TipPackages.SingleOrDefaultAsync(m => m.ID == id);
            if (tipPackage == null)
            {
                return NotFound();
            }

            return View(tipPackage);
        }

        // POST: TipPackages/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipPackage = await _context.TipPackages.SingleOrDefaultAsync(m => m.ID == id);
            _context.TipPackages.Remove(tipPackage);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TipPackageExists(int id)
        {
            return _context.TipPackages.Any(e => e.ID == id);
        }
    }
}
