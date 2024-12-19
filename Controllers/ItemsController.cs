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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }

        // GET: Items/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        // GET: Items/HogsmeadeShop
        public async Task<IActionResult> HogsmeadeShop()
        {
            
            if (_context == null)
            {
                _logger.LogError("DbContext tidak diinisialisasi dengan benar.");
                return View("Error");
            }

            var items = await _context.Items.ToListAsync();

            if (items == null || !items.Any())
            {
                _logger.LogWarning("Tabel Items tidak memiliki data.");
            }

            return View(items);  
        }

        // GET: Items/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            var item = await _context.Items.FindAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id, [Bind("ItemId,NamaItem,Deskripsi,URLGambar,Stok,Harga")] Item item)
        {
            if (id == Guid.Empty) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
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
        [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Purchase(Guid itemId , int Amount)
        {
            if (itemId == Guid.Empty) return NotFound();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.NamaLengkap == HttpContext.User.Identity.Name);

            if (user == null) return NotFound();

            var item = await _context.Items.FindAsync(itemId);
            if (item == null) return NotFound();

            if (user.SaldoDigigall < item.Harga * Amount)
            {
                ViewBag.ErrorMessage = "Saldo Anda tidak cukup untuk membeli item ini.";
                return View("HogsmeadeShop", await _context.Items.ToListAsync());
            }
            else
            {
                user.SaldoDigigall -= item.Harga * Amount;
                item.Stok -= 1 * Amount;

                _context.Users.Update(user);
                _context.Items.Update(item);

                await _context.SaveChangesAsync();


                var transaksi = new Transaksi()
                {
                    ItemId = itemId,
                    UserId = user.UserId,
                    TotalHarga = item.Harga * Amount,
                    JumlahPembelian = Amount,
                    TanggalTransaksi = DateTime.Now
                };


                _context.Transaksis.Add(transaksi);
                await _context.SaveChangesAsync();

            }

            return RedirectToAction(nameof(HogsmeadeShop));
        }

        private bool ItemExists(Guid id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
