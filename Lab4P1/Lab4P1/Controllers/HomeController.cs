using Lab4P1.Data;
using Microsoft.AspNetCore.Mvc;

namespace Lab4P1.Controllers
{
    public class HomeController : Controller
    {
        private readonly SportsDbContext _context;

        public HomeController(SportsDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}
