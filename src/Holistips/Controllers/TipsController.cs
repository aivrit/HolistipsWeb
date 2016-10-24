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
    public class TipsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Tips
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tips.Include(t => t.TipPackage);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tip = await _context.Tips.SingleOrDefaultAsync(m => m.ID == id);
            if (tip == null)
            {
                return NotFound();
            }

            return View(tip);
        }

        // GET: Tips/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["TipPackageID"] = new SelectList(_context.TipPackages, "ID", "ID");
            return View();
        }

        // POST: Tips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TipAnalogy,TipAuthorID,TipCreationDate,TipExplanation,TipHashtags,TipPackageID,TipRefs,TipTitle,TipWhenTo,TipWhere")] Tip tip)
        {
            if (ModelState.IsValid)
            {
                tip.TipCreationDate = DateTime.Now;
                _context.Add(tip);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["TipPackageID"] = new SelectList(_context.TipPackages, "ID", "ID", tip.TipPackageID);
            return View(tip);
        }


        // GET: Tips/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tip = await _context.Tips.SingleOrDefaultAsync(m => m.ID == id);
            if (tip == null)
            {
                return NotFound();
            }
            ViewData["TipPackageID"] = new SelectList(_context.TipPackages, "ID", "ID", tip.TipPackageID);
            return View(tip);
        }


        // POST: Tips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TipAnalogy,TipAuthorID,TipCreationDate,TipExplanation,TipHashtags,TipPackageID,TipRefs,TipTitle,TipWhenTo,TipWhere")] Tip tip)
        {
            if (id != tip.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipExists(tip.ID))
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
            ViewData["TipPackageID"] = new SelectList(_context.TipPackages, "ID", "ID", tip.TipPackageID);
            return View(tip);
        }


        // GET: Tips/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tip = await _context.Tips.SingleOrDefaultAsync(m => m.ID == id);
            if (tip == null)
            {
                return NotFound();
            }

            return View(tip);
        }

        // POST: Tips/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tip = await _context.Tips.SingleOrDefaultAsync(m => m.ID == id);
            _context.Tips.Remove(tip);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TipExists(int id)
        {
            return _context.Tips.Any(e => e.ID == id);
        }
    }
}
