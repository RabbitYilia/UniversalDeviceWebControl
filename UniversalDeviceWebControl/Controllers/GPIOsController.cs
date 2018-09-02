using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversalDeviceWebControl.Data;
using UniversalDeviceWebControl.Models;

namespace UniversalDeviceWebControl.Controllers
{
    public class GPIOsController : Controller
    {
        private readonly WebDBContext _context;

        public GPIOsController(WebDBContext context)
        {
            _context = context;
        }

        // GET: GPIOs
        public async Task<IActionResult> Index()
        {
            return View(await _context.GPIO.ToListAsync());
        }

        // GET: GPIOs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gPIO = await _context.GPIO
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gPIO == null)
            {
                return NotFound();
            }

            return View(gPIO);
        }

        // GET: GPIOs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GPIOs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Pin,Type,CurrentValue")] GPIO gPIO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gPIO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gPIO);
        }

        // GET: GPIOs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gPIO = await _context.GPIO.FindAsync(id);
            if (gPIO == null)
            {
                return NotFound();
            }
            return View(gPIO);
        }

        // POST: GPIOs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Pin,Type,CurrentValue")] GPIO gPIO)
        {
            if (id != gPIO.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gPIO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GPIOExists(gPIO.ID))
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
            return View(gPIO);
        }

        // GET: GPIOs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gPIO = await _context.GPIO
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gPIO == null)
            {
                return NotFound();
            }

            return View(gPIO);
        }

        // POST: GPIOs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gPIO = await _context.GPIO.FindAsync(id);
            _context.GPIO.Remove(gPIO);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GPIOExists(int id)
        {
            return _context.GPIO.Any(e => e.ID == id);
        }
    }
}
