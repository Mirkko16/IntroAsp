using IntroAsp.Models;
using Microsoft.AspNetCore.Mvc;
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

    }
}
