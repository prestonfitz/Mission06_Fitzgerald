using Microsoft.AspNetCore.Mvc;
using Mission06_Fitzgerald.Models;
using System.Diagnostics;

namespace Mission06_Fitzgerald.Controllers
{
    public class HomeController : Controller
    {
        private NewMovieContext _context;
        public HomeController(NewMovieContext NMC)
        {
            _context = NMC;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            ViewBag.Categories = _context.Categories.ToList();

            return View("AddMovie", new NewMovie());
        }

        [HttpPost]
        public IActionResult AddMovie(NewMovie response)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(response);
                _context.SaveChanges();

                return View("Confirmation", response);
            }
            else
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View(response);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}