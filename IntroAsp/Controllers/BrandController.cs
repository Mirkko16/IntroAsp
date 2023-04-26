using IntroAsp.Models;
using IntroAsp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IntroAsp.Controllers
{
    public class BrandController : Controller
    {

        private readonly PubContext _context;

        //asi obtengo objetos inyectados en program
        public BrandController(PubContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
            => View(await _context.Brands.ToListAsync());


        public IActionResult Create()
        {
            ViewData["Brands"] = new SelectList(_context.Brands, "BrandId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                var brand = new Brand()
                {
                    Name = model.Name,
                    BrandId = model.BrandId
                };
                _context.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Brands"] = new SelectList(_context.Brands, "BrandId", "Name", model.BrandId);
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            var model = new BrandViewModel
            {
                BrandId = brand.BrandId,
                Name = brand.Name
            };
            ViewData["Brands"] = new SelectList(_context.Brands, "BrandId", "Name", model.BrandId);
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                var brand = await _context.Brands.FindAsync(model.BrandId);
                if (brand == null)
                {
                    return NotFound();
                }
                brand.Name = model.Name;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Brands"] = new SelectList(_context.Brands, "BrandId", "Name", model.BrandId);
            return View(model);
        }


    }
}
