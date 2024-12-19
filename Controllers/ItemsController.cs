using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DigiGall.Data;
using DigiGall.Models;

namespace DigiGall.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(ApplicationDbContext context, ILogger<ItemsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }

        // GET: Items/Create
        public async Task<IActionResult> Create()
        {
            // Periksa apakah pengguna adalah Admin
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction(nameof(Index));  // Arahkan Admin ke halaman Index
            }
            else
            {
                return RedirectToAction(nameof(HogsmeadeShop));  // Arahkan selain Admin ke halaman HogsmeadeShop
            }
        }

        // GET: Items/HogsmeadeShop
        public async Task<IActionResult> HogsmeadeShop()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction(nameof(Index));  // Arahkan Admin ke halaman Index
            }
            if (_context == null)
            {
                _logger.LogError("DbContext tidak diinisialisasi dengan benar.");
                return View("Error");
            }

            // Ambil seluruh data dari tabel Items
            var items = await _context.Items.ToListAsync();

            if (items == null || !items.Any())
            {
                _logger.LogWarning("Tabel Items tidak memiliki data.");
            }

            return View(items);  // Mengirimkan data Items ke tampilan
        }

        // GET: Items/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var item = await _context.Items.FindAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id, [Bind("NamaItem,Deskripsi,URLGambar,Stok,Harga")] Item item)
        {
            if (id != item.NamaItem)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.NamaItem))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NamaItem,Deskripsi,URLGambar,Stok,Harga")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.NamaItem == id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(string id)
        {
            return _context.Items.Any(e => e.NamaItem == id);
        }
    }
}
