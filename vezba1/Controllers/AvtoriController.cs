using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vezba1.Data;
using vezba1.Models;

namespace vezba1.Controllers
{
    public class AvtoriController : Controller
    {
        private readonly AppDbContext _context;
        public AvtoriController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString, string searchNationality)
        {
            IEnumerable<Avtor> data;

            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchNationality))
            {
                data = await _context.Avtori
                    .Where(a => (a.Ime.Contains(searchString) || a.Prezime.Contains(searchString)) && a.Nacionalnost.Contains(searchNationality))
                    .ToListAsync();
            }
            else if (!String.IsNullOrEmpty(searchString))
            {
                data = await _context.Avtori
                    .Where(a => a.Ime.Contains(searchString) || a.Prezime.Contains(searchString))
                    .ToListAsync();
            }
            else if (!String.IsNullOrEmpty(searchNationality))
            {
                data = await _context.Avtori
                    .Where(a => a.Nacionalnost.Contains(searchNationality))
                    .ToListAsync();
            }
            else
            {
                data = await _context.Avtori.ToListAsync();
            }
            return View(data);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var avtor = _context.Avtori.Find(id);
            if (avtor == null)
            {
                return NotFound();
            }
            return View(avtor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Avtor avtor)
        {
            if (ModelState.IsValid)
            {
                _context.Update(avtor);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(avtor);
        }
        // GET: Avtori/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Avtori/Create
        [HttpPost]
        public async Task<IActionResult> Create(Avtor avtor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(avtor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(avtor);
        }
        // GET: Avtori/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var avtor = await _context.Avtori.FindAsync(id);
            if (avtor == null)
            {
                return NotFound();
            }

            return View(avtor);
        }
        // GET: Avtori/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var avtor = await _context.Avtori.FindAsync(id);
            if (avtor == null)
            {
                return NotFound();
            }

            return View(avtor);
        }
        // POST: Avtori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avtor = await _context.Avtori.FindAsync(id);
            if (avtor == null)
            {
                return NotFound();
            }
            _context.Avtori.Remove(avtor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
