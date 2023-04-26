using IntroAsp.Models;
using IntroAsp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IntroAsp.Controllers
{
    public class BeerController : Controller
    {
        private readonly PubContext _context;

        public BeerController(PubContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var beers = _context.Beers.Include(b => b.Brand);
            return View(await beers.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Brands"] = new SelectList(_context.Brands, "BrandId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BeerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var beer = new Beer()
                {
                    Name = model.Name,
                    BrandId = Convert.ToInt32(model.BrandId)
                };
                _context.Add(beer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Brands"] = new SelectList(_context.Brands, "BrandId", "Name", model.BrandId);
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beers
                .Include(b => b.Brand)
                .FirstOrDefaultAsync(m => m.BeerId == id);
            if (beer == null)
            {
                return NotFound();
            }

            return View(beer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var beer = await _context.Brands.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }
            var model = new BeerViewModel
            {
                BeerId = id,
                Name = beer.Name,
                BrandId = beer.BrandId
            };
            ViewData["Beers"] = new SelectList(_context.Beers, "BeerId", "Name", model.BrandId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BeerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var beer = await _context.Beers.FindAsync(model.BeerId);
                if (beer == null)
                {
                    return NotFound();
                }
                beer.Name = model.Name;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Beers"] = new SelectList(_context.Beers, "BeerId", "Name", model.BrandId);
            return View(model);
        }
    }
}
