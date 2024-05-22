using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using vezba1.Data;
using vezba1.Models;
using vezba1.viewModel;

namespace vezba1.Controllers
{
    public class KnigiController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public KnigiController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(string knigaZanr, string searchStringN, string searchStringG, string searchStringI)
        {
            IQueryable<Kniga> knigi = _context.Knigi.AsQueryable();
            IQueryable<string> genreQuery = _context.Knigi.OrderBy(m => m.Zanr).Select(m => m.Zanr).Distinct();

            if (!string.IsNullOrEmpty(searchStringN))
            {
                knigi = knigi.Where(s => s.Naslov.Contains(searchStringN));
            }
            if (!string.IsNullOrEmpty(searchStringG))
            {
                if (int.TryParse(searchStringG, out int year))
                {
                    knigi = knigi.Where(s => s.Godina == year);
                }
            }
            if (!string.IsNullOrEmpty(searchStringI))
            {
                knigi = knigi.Where(s => s.Izdavac.Contains(searchStringI));
            }
            if (!string.IsNullOrEmpty(knigaZanr))
            {
                knigi = knigi.Where(x => x.Zanr == knigaZanr);
            }

            knigi = knigi.Include(m => m.AvtorKnigi).ThenInclude(m => m.Avtor);

            var bookGenreVM = new KnigaZanrViewModel
            {
                Zanrovi = new SelectList(await genreQuery.ToListAsync()),
                Knigi = await knigi.ToListAsync()
            };

            return View(bookGenreVM);
        }
        // GET: Knigas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kniga = await _context.Knigi
                .Include(m => m.AvtorKnigi)
                .ThenInclude(m => m.Avtor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (kniga == null)
            {
                return NotFound();
            }

            return View(kniga);
        }
        // GET: Knigas/Create
        [HttpGet]
        public IActionResult Create()
        {
            var avtorList = _context.Avtori.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = $"{g.Ime} {g.Prezime}" }).ToList();
            return View(new AvtorKnigaEditViewModel { AvtorList = avtorList });
        }

        // POST: Knigas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AvtorKnigaEditViewModel bookVM)
        {
            if (ModelState.IsValid)
            {
                if (bookVM.Kniga.SlikaFile != null && bookVM.Kniga.SlikaFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(bookVM.Kniga.SlikaFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await bookVM.Kniga.SlikaFile.CopyToAsync(fileStream);
                    }
                    bookVM.Kniga.SlikaUrl = uniqueFileName;
                }
                _context.Add(bookVM.Kniga);
                await _context.SaveChangesAsync();
                if (bookVM.SelectedAvtors != null && bookVM.SelectedAvtors.Any())
                {
                    foreach (var avtorId in bookVM.SelectedAvtors)
                    {
                        _context.AvtorKnigi.Add(new AvtorKniga { KnigaId = bookVM.Kniga.Id, AvtorId = avtorId });
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AvtorId"] = new SelectList(_context.Set<Avtor>(), "Id", "FullName");
            return View(bookVM);
        }
        // GET: Knigas/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kniga = await _context.Knigi
                .Include(m => m.AvtorKnigi)
                .ThenInclude(m => m.Avtor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (kniga == null)
            {
                return NotFound();
            }

            // Load all authors into memory first and then sort them by FullName
            var avtors = await _context.Avtori.ToListAsync();
            avtors = avtors.OrderBy(s => s.FullName).ToList();

            AvtorKnigaEditViewModel viewmodel = new AvtorKnigaEditViewModel
            {
                Kniga = kniga,
                AvtorList = new MultiSelectList(avtors, "Id", "FullName"),
                SelectedAvtors = kniga.AvtorKnigi.Select(sa => sa.AvtorId).ToList()
            };

            return View(viewmodel);
        }

        // POST: Knigas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AvtorKnigaEditViewModel viewmodel)
        {
            if (id != viewmodel.Kniga.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (viewmodel.Kniga.SlikaFile != null && viewmodel.Kniga.SlikaFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(viewmodel.Kniga.SlikaFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await viewmodel.Kniga.SlikaFile.CopyToAsync(fileStream);
                        }
                        viewmodel.Kniga.SlikaUrl = uniqueFileName;
                    }
                    _context.Update(viewmodel.Kniga);
                    await _context.SaveChangesAsync();

                    var newAvtorList = viewmodel.SelectedAvtors;
                    var prevAvtorList = _context.AvtorKnigi.Where(s => s.KnigaId == id).Select(s => s.AvtorId).ToList();

                    var toBeRemoved = _context.AvtorKnigi.Where(s => s.KnigaId == id && !newAvtorList.Contains(s.AvtorId)).ToList();
                    _context.AvtorKnigi.RemoveRange(toBeRemoved);

                    foreach (var avtorId in newAvtorList)
                    {
                        if (!prevAvtorList.Contains(avtorId))
                        {
                            _context.AvtorKnigi.Add(new AvtorKniga { AvtorId = avtorId, KnigaId = id });
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnigaExists(viewmodel.Kniga.Id))
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

            return View(viewmodel);
        }

        // GET: Knigas/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kniga = await _context.Knigi.Include(b => b.AvtorKnigi).ThenInclude(b => b.Avtor).FirstOrDefaultAsync(m => m.Id == id);
            if (kniga == null)
            {
                return NotFound();
            }

            return View(kniga);
        }

        // POST: Knigas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kniga = await _context.Knigi.FindAsync(id);
            if (kniga != null)
            {
                _context.Knigi.Remove(kniga);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        private bool KnigaExists(int id)
        {
            return _context.Knigi.Any(e => e.Id == id);
        }

        public async Task<IActionResult> KnigiodAvtor(int? authorId)
        {
            if (authorId == null)
            {
                return NotFound();
            }

            var knigiOdAvtor = await _context.Knigi
                .Include(k => k.AvtorKnigi)
                .ThenInclude(ak => ak.Avtor)
                .Where(k => k.AvtorKnigi.Any(ak => ak.AvtorId == authorId))
                .ToListAsync();

            return View(knigiOdAvtor);
        }
    }
}
