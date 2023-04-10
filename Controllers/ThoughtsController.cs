using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Thoujour.Models;

namespace Thoujour.Controllers
{
    public class ThoughtsController : Controller
    {
        private readonly ThoughtsDb _context;

        public ThoughtsController(ThoughtsDb context)
        {
            _context = context;
        }

        // GET: Thoughts
        public async Task<IActionResult> Index(int? id)
        {
            if (_context.Thoughts != null)
            {
                List<Thought> thoughts = await _context.Thoughts.ToListAsync();

                //thoughts.Clear();

                if (thoughts.Count > 0)
                {
                    id = id ?? thoughts.FirstOrDefault()?.Id;
                    (thoughts.Find(t => t.Id == id) ?? new Thought()).Blocks = await _context.Blocks.Where(b => b.ThoughtId == id).ToListAsync();
                    (thoughts.Find(t => t.Id == id) ?? new Thought()).Comments = await _context.Comments.Where(b => b.ThoughtId == id).ToListAsync();
                }

                ViewData["Id"] = id;

                return View(thoughts);
            }
            else
                return Problem("Entity set 'ThoughtsDb.Thought'  is null.");
        }

        // GET: Thoughts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Thoughts == null)
            {
                return NotFound();
            }

            var thought = await _context.Thoughts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (thought == null)
            {
                return NotFound();
            }

            return View(thought);
        }

        // GET: Thoughts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Thoughts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] Thought thought)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thought);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(thought);
        }

        // GET: Thoughts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Thoughts == null)
            {
                return NotFound();
            }

            var thought = await _context.Thoughts.FindAsync(id);
            if (thought == null)
            {
                return NotFound();
            }
            return View(thought);
        }

        // POST: Thoughts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] Thought thought)
        {
            if (id != thought.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thought);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThoughtExists(thought.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(BlocksEdit), thought);
            }
            return View(thought);
        }

        [HttpGet]
        public async Task<IActionResult> BlocksEdit(int? id)
        {
            if (id == null || _context.Thoughts == null)
            {
                return NotFound();
            }

            var thought = await _context.Thoughts.FindAsync(id);
            if (thought == null)
            {
                return NotFound();
            }

            thought.Blocks = await _context.Blocks.Where(b => b.ThoughtId == id).ToListAsync();

            return View(thought);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlocksEdit(int id, [Bind("Id,Blocks")] Thought thought)
        {
            if (id != thought.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thought);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThoughtExists(thought.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(thought);
        }

        [HttpPost]
        public async Task<IActionResult> AddBlock([Bind("Id,ThoughtId,Title,Text")] Block block, IFormFile b64Img)
        {
            if (block == null)
            {
                return NotFound();
            }

            if (b64Img != null)
                using (MemoryStream ms = new MemoryStream())
                {
                    await b64Img.CopyToAsync(ms);
                    block.B64Img = Convert.ToBase64String(ms.ToArray());
                }

            if (await _context.Blocks.AnyAsync(b => b.Id == block.Id))
                _context.Update(block);
            else
                _context.Add(block);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(BlocksEdit), await _context.Thoughts.FindAsync(block.ThoughtId));
        }

        // GET: Thoughts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Thoughts == null)
            {
                return NotFound();
            }

            var thought = await _context.Thoughts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (thought == null)
            {
                return NotFound();
            }

            return View(thought);
        }

        // POST: Thoughts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Thoughts == null)
            {
                return Problem("Entity set 'ThoughtsDb.Thought'  is null.");
            }
            var thought = await _context.Thoughts.FindAsync(id);
            if (thought != null)
            {
                _context.Thoughts.Remove(thought);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThoughtExists(int id)
        {
            return (_context.Thoughts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
