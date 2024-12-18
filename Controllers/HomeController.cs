using DigiGall.Models;
using DigiGall.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DigiGall.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger , ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

            public async Task<IActionResult> Index()
        {
            // Pastikan context tidak null
            if (_context == null)
            {
                _logger.LogError("DbContext tidak diinisialisasi dengan benar.");
                return View("Error");  // Menampilkan halaman error jika DbContext null
            }

            // Cek apakah tabel Quests kosong
            var quest = await _context.Quests.ToListAsync();
            if (quest == null || !quest.Any())
            {
                _logger.LogWarning("Tabel Quests tidak memiliki data.");
            }

            return View(quest);  // Mengirimkan data quest ke tampilan
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
