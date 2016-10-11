using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Holistips.Data;
using Holistips.Models;

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
            return View(await _context.Tips.ToListAsync());
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TipAnalogy,TipExplanation,TipHashtags,TipRefs,TipTitle,TipWhenTo,TipWhere")] Tip tip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tip);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tip);
        }

        // GET: Tips/Edit/5
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
            return View(tip);
        }

        // POST: Tips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TipAnalogy,TipExplanation,TipHashtags,TipRefs,TipTitle,TipWhenTo,TipWhere")] Tip tip)
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
            return View(tip);
        }

        // GET: Tips/Delete/5
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
