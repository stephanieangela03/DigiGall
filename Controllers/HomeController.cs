using DigiGall.Models;
using DigiGall.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace DigiGall.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (_context == null)
            {
                _logger.LogError("DbContext tidak diinisialisasi dengan benar.");
                return View("Error");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.NamaLengkap == HttpContext.User.Identity.Name);

            
            var quests = await _context.Quests.ToListAsync();
            var questStatusList = new List<QuestStatusViewModel>();

            foreach (var quest in quests)
            {
                var isTaken = false;
                if (user != null)
                {
                    isTaken = await _context.PemberianQuests.AnyAsync(pq => pq.QuestId == quest.QuestId && pq.UserId == user.UserId);
                }
                

                questStatusList.Add(new QuestStatusViewModel
                {
                    Quest = quest,
                    IsTaken = isTaken
                });
            }

            return View(questStatusList);
        }


        public async Task<IActionResult> Quest(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var quest = await _context.Quests.FindAsync(id);
            if (quest == null)
            {
                return NotFound();
            }
            return RedirectToAction("Detail", "Quest", new { id });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
