using DigiGall.Data;
using DigiGall.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigiGall.Controllers
{
    public class HistoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Quest()
        {
            var userName = HttpContext.User.Identity?.Name;
            if (userName == null) return NotFound();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.NamaLengkap == userName);
            if (user == null) return NotFound();

            var pemberianQuestsHistory = await _context.PemberianQuests.Where(pq => pq.UserId == user.UserId).ToListAsync();
            var questIds = pemberianQuestsHistory.Select(pq => pq.QuestId).Distinct();
            var questHistory = await _context.Quests.Where(q => questIds.Contains(q.QuestId)).ToListAsync();

            var questList = new QuestHistoryViewModel()
            {
                PemberianQuests = pemberianQuestsHistory,
                Quests = questHistory
            };

            return View(questList);
        }

        public async Task<IActionResult> Transaction()
        {
            var userName = HttpContext.User.Identity?.Name;
            if (userName == null) return NotFound();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.NamaLengkap == userName);
            if (user == null) return NotFound();

            var transactions = await _context.Transaksis.Where(t => t.UserId == user.UserId).ToListAsync();
            var itemIds = transactions.Select(t => t.ItemId).Distinct();
            var items = await _context.Items.Where(i => itemIds.Contains(i.ItemId)).ToListAsync();

            var transactionHistoryViewModel = new TransactionHistoryViewModel
            {
                Transaksis = transactions,
                Items = items
            };

            return View(transactionHistoryViewModel);
        }
    }
}
