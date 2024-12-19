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

            // Cek apakah tabel PemberianQuests kosong
            var pemberianQuests = await _context.PemberianQuests
                .Include(pq => pq.Quest) // Join ke tabel Quest
                .Include(pq => pq.User) // Join ke tabel User
                .Select(pq => new PemberianQuestViewModel
                {
                    NamaPembuat = pq.User.NamaLengkap, // Nama dari pembuat quest
                    NamaQuest = pq.Quest.NamaQuest, // Nama dari quest
                    Reward = pq.Quest.Reward.ToString(), // Reward dari quest
                    Deadline = pq.Quest.Deadline // Deadline quest
                })
                .ToListAsync();

            if (pemberianQuests == null || !pemberianQuests.Any())
            {
                _logger.LogWarning("Tabel PemberianQuests tidak memiliki data.");
            }

            return View(pemberianQuests);  // Mengirimkan data ViewModel ke tampilan
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
