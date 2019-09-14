using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMerchTrack2.Data;
using MyMerchTrack2.Models;

namespace MyMerchTrack2.Controllers
{
    [Authorize]
    public class MerchSizesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MerchSizesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MerchSizes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MerchSize.Include(m => m.Merch);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MerchSizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var merchSize = await _context.MerchSize
                .Include(m => m.Merch)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (merchSize == null)
            {
                return NotFound();
            }

            return View(merchSize);
        }

        // GET: MerchSizes/Create
        public IActionResult Create()
        {
            ViewData["MerchId"] = new SelectList(_context.Merch, "Id", "MerchDescription");
            return View();
        }

        // POST: MerchSizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Size,MerchId,Quantity")] MerchSize merchSize)
        {
            if (ModelState.IsValid)
            {
                _context.Add(merchSize);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MerchId"] = new SelectList(_context.Merch, "Id", "MerchDescription", merchSize.MerchId);
            return View(merchSize);
        }

        // GET: MerchSizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var merchSize = await _context.MerchSize.FindAsync(id);
            if (merchSize == null)
            {
                return NotFound();
            }
            ViewData["MerchId"] = new SelectList(_context.Merch, "Id", "MerchDescription", merchSize.MerchId);
            return View(merchSize);
        }

        // POST: MerchSizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Size,MerchId,Quantity")] MerchSize merchSize)
        {
            if (id != merchSize.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(merchSize);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MerchSizeExists(merchSize.Id))
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
            ViewData["MerchId"] = new SelectList(_context.Merch, "Id", "MerchDescription", merchSize.MerchId);
            return View(merchSize);
        }

        // GET: MerchSizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var merchSize = await _context.MerchSize
                .Include(m => m.Merch)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (merchSize == null)
            {
                return NotFound();
            }

            return View(merchSize);
        }

        // POST: MerchSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var merchSize = await _context.MerchSize.FindAsync(id);
            _context.MerchSize.Remove(merchSize);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MerchSizeExists(int id)
        {
            return _context.MerchSize.Any(e => e.Id == id);
        }
    }
}
