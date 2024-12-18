using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigiGall.Data;
using DigiGall.Models;

namespace DigiGall.Controllers
{
    public class RiwayatTransaksiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RiwayatTransaksiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RiwayatTransaksis
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RiwayatTransaksis.Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RiwayatTransaksis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riwayatTransaksi = await _context.RiwayatTransaksis
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (riwayatTransaksi == null)
            {
                return NotFound();
            }

            return View(riwayatTransaksi);
        }

        // GET: RiwayatTransaksis/Create
        public IActionResult RiwayatTransaksi()
        {
            //ViewData["Email"] = new SelectList(_context.Users, "Email", "Email");
            return View();
        }

        // POST: RiwayatTransaksis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NamaTransaksi,TipeTransaksi,TotalHarga,Status,NamaPenerima,NamaPengirim,TanggalTransaksi,Email")] RiwayatTransaksi riwayatTransaksi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(riwayatTransaksi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email", riwayatTransaksi.Email);
            return View(riwayatTransaksi);
        }

        // GET: RiwayatTransaksis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riwayatTransaksi = await _context.RiwayatTransaksis.FindAsync(id);
            if (riwayatTransaksi == null)
            {
                return NotFound();
            }
            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email", riwayatTransaksi.Email);
            return View(riwayatTransaksi);
        }

        // POST: RiwayatTransaksis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NamaTransaksi,TipeTransaksi,TotalHarga,Status,NamaPenerima,NamaPengirim,TanggalTransaksi,Email")] RiwayatTransaksi riwayatTransaksi)
        {
            if (id != riwayatTransaksi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(riwayatTransaksi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RiwayatTransaksiExists(riwayatTransaksi.Id))
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
            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email", riwayatTransaksi.Email);
            return View(riwayatTransaksi);
        }

        // GET: RiwayatTransaksis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var riwayatTransaksi = await _context.RiwayatTransaksis
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (riwayatTransaksi == null)
            {
                return NotFound();
            }

            return View(riwayatTransaksi);
        }

        // POST: RiwayatTransaksis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var riwayatTransaksi = await _context.RiwayatTransaksis.FindAsync(id);
            if (riwayatTransaksi != null)
            {
                _context.RiwayatTransaksis.Remove(riwayatTransaksi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RiwayatTransaksiExists(int id)
        {
            return _context.RiwayatTransaksis.Any(e => e.Id == id);
        }
    }
}
