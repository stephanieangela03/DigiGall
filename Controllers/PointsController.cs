using DigiGall.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigiGall.Controllers
{
    public class PointsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PointsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Manage()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> updatePoint(Guid userId, decimal point)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            user.SaldoDigigall = point;
            _context.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Manage));
        }
    }
}