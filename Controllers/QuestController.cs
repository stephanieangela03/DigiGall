using DigiGall.Data;
using DigiGall.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class QuestController : Controller
{
    private readonly ApplicationDbContext _context;

    public QuestController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Quest
    //[Authorize]
    public async Task<IActionResult> Index()
    {
        var quests = await _context.Quests.ToListAsync();
        return View(quests);
    }

    // GET: Quest/Create
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Quest/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([Bind("NamaQuest,Kriteria,Deskripsi,Reward,Deadline")] Quest quest)
    {
        if (ModelState.IsValid)
        {
            _context.Add(quest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(quest);
    }

    // GET: Quest/Edit/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(string id)
    {
        if (string.IsNullOrEmpty(id))
            return NotFound();

        var quest = await _context.Quests.FindAsync(id);
        if (quest == null)
            return NotFound();

        return View(quest);
    }

    // POST: Quest/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(string id, [Bind("NamaQuest,Kriteria,Deskripsi,Reward,Deadline")] Quest quest)
    {
        if (id != quest.NamaQuest)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(quest);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestExists(quest.NamaQuest))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(quest);
    }

    // GET: Quest/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrEmpty(id))
            return NotFound();

        var quest = await _context.Quests
            .FirstOrDefaultAsync(m => m.NamaQuest == id);

        if (quest == null)
            return NotFound();

        return View(quest);
    }

    // POST: Quest/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var quest = await _context.Quests.FirstOrDefaultAsync(q => q.NamaQuest == id);
        if (quest != null)
        {
            _context.Quests.Remove(quest);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> TakeQuest(string id)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized(); 
        }

        var quest = await _context.Quests.FindAsync(id);
        if (quest == null)
        {
            return NotFound(); 
        }

        //var userQuest = new PemberianQuest()
        //{
        //    QuestId = quest.Id,
        //    UserId = userId,
        //    TakenAt = DateTime.UtcNow
        //};

        //_context.UserQuests.Add(userQuest);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index)); // Redirect back to quest list
    }


    private bool QuestExists(string id)
    {
        return _context.Quests.Any(e => e.NamaQuest == id);
    }
}
