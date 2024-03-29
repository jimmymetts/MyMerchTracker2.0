﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMerchTrack2.Data;
using MyMerchTrack2.Models;


namespace MyMerchTrack2.Controllers


{
    [Authorize]
    public class MerchesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MerchesController(ApplicationDbContext context,
                          UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Merches
        public async Task<IActionResult> Index(int? merchType)
        {
            var user = await GetCurrentUserAsync();
            var merch = _context.Merch
             .Where(m => m.ApplicationUserId == user.Id)
             //.Include(m => m.ApplicationUserId)
             .Include(m => m.MerchType);

            if (merchType != null)
            {
                var merchCategory = merch.Where(m => m.MerchTypeId == merchType);
                return View(await merchCategory.ToListAsync());
            }
            else
            { 

            return View(await merch.ToListAsync());
    }
}

        /*Include(m => m.MerchType);*/


        // GET: Merches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var merch = await _context.Merch
                .Include(m => m.MerchType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (merch == null)
            {
                return NotFound();
            }

            return View(merch);

           
        }

        // GET: Merches/Create
        public IActionResult Create()
        {
            ViewData["MerchTypeId"] = new SelectList(_context.Set<MerchType>(), "Id", "Title");
            return View();
        }

        // POST: Merches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MerchDescription,MerchPrice,UserId,ImagePath,MerchTypeId")] Merch merch)
        {
            //line to attach user's Id to merches userid

            // few line of code here to convert viewmodel to data model

            var user = await GetCurrentUserAsync();
            merch.ApplicationUserId = user.Id;

            ModelState.Remove("ApplicationUserId");

            if (ModelState.IsValid)
            {
                _context.Add(merch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MerchTypeId"] = new SelectList(_context.Set<MerchType>(), "Id", "Title", merch.MerchTypeId);
            return View(merch);
        }

        // GET: Merches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var merch = await _context.Merch.FindAsync(id);
            if (merch == null)
            {
                return NotFound();
            }
            ViewData["MerchTypeId"] = new SelectList(_context.Set<MerchType>(), "Id", "Title", merch.MerchTypeId);
            return View(merch);
        }

        // POST: Merches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MerchDescription,MerchPrice,ApplicationUserId,ImagePath,MerchTypeId")] Merch merch)
        {
            
            if (id != merch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(merch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MerchExists(merch.Id))
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
            ViewData["MerchTypeId"] = new SelectList(_context.Set<MerchType>(), "Id", "Title", merch.MerchTypeId);

            return View(merch);


        }

        // GET: Merches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var merch = await _context.Merch
                .Include(m => m.MerchType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (merch == null)
            {
                return NotFound();
            }

            return View(merch);
        }

        // POST: Merches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var merch = await _context.Merch.FindAsync(id);
            _context.Merch.Remove(merch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MerchExists(int id)
        {
            return _context.Merch.Any(e => e.Id == id);
        }
    }
}
