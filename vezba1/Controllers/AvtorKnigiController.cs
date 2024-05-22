using Microsoft.AspNetCore.Mvc;
using vezba1.Data;

namespace vezba1.Controllers
{
    public class AvtorKnigiController : Controller
    {
        private readonly AppDbContext _context;
        public AvtorKnigiController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var data= _context.AvtorKnigi.ToList();
            return View(data);
        }
    }
}
