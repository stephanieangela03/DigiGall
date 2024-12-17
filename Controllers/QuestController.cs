using DigiGall.Data;
using DigiGall.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class QuestController : Controller
{
    private readonly ApplicationDbContext _context;

    public QuestController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Quest
    public async Task<IActionResult> Index()
    {
        var quests = await _context.Quests.ToListAsync();
        return View(quests);
    }

    // GET: Quest/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Quest/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
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
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var quest = await _context.Quests.FindAsync(id);
        if (quest != null)
        {
            _context.Quests.Remove(quest);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool QuestExists(string id)
    {
        return _context.Quests.Any(e => e.NamaQuest == id);
    }
}
