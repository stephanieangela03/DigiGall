using DigiGall.Data;
using DigiGall.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([Bind("NamaQuest,Kriteria,Deskripsi,Reward,Deadline")] Quest quest)
    {
        if (ModelState.IsValid)
        {
            quest.Creator = HttpContext.User.Identity.Name;
            
            _context.Add(quest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(quest);
    }


    // GET: Quest/Edit/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(Guid id)
    {
        if (id == Guid.Empty)
        {
            return NotFound();
        }

        var quest = await _context.Quests.FindAsync(id);
        if (quest == null)
            return NotFound();

        return View(quest);
    }

    // POST: Quest/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(Guid id, [Bind("QuestId,NamaQuest,Kriteria,Deskripsi,Reward,Deadline")] Quest quest)
    {
        if (id != quest.QuestId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(quest);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestExists(quest.QuestId))
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
    public async Task<IActionResult> Delete(Guid id)
    {
        if (id == Guid.Empty) return NotFound();

        var quest = await _context.Quests
            .FirstOrDefaultAsync(m => m.QuestId == id);

        if (quest == null)
            return NotFound();

        return View(quest);
    }

    // POST: Quest/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (id == Guid.Empty) return NotFound();

        var quest = await _context.Quests.FindAsync(id);
        if (quest != null)
        {
            _context.Quests.Remove(quest);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> TakeQuest(Guid id)
    {
        if (id == Guid.Empty) return NotFound();

        var quest = await _context.Quests.FindAsync(id);
        if (quest == null) return NotFound();



        var user = await _context.Users.FirstOrDefaultAsync(u => u.NamaLengkap == HttpContext.User.Identity.Name);
        if (user == null) return NotFound();

        var pemberianQuest = new PemberianQuest
        {
            PemberianQuestId = Guid.NewGuid(),
            TanggalSelesai = quest.Deadline,
            TanggalMulai = DateTime.Now,
            Status = "In Progress",
            QuestId = quest.QuestId,
            UserId = user.UserId
        };

        _context.PemberianQuests.Add(pemberianQuest);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }

    private bool QuestExists(Guid id)
    {
        return _context.Quests.Any(e => e.QuestId == id);
    }


    public async Task<IActionResult> Detail(Guid id)
    {
        if (id == Guid.Empty) return NotFound();
        var quest = await _context.Quests.FindAsync(id);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.NamaLengkap == HttpContext.User.Identity.Name);
        if (user == null) return NotFound();

        var isTaken = await _context.PemberianQuests.AnyAsync(pq => pq.QuestId == quest.QuestId && pq.UserId == user.UserId);


        var QuestStatus =  new QuestStatusViewModel()
        {
            Quest = quest,
            IsTaken = isTaken
        };


        if (quest == null) return NotFound();
        return View(QuestStatus);
    }
}
