using Microsoft.AspNetCore.Mvc;
using Mission06_Fitzgerald.Models;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

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

        public IActionResult MovieList()
        {
            //linq to get data
            //var movies = _context.Movies.Where(x => x.Director is not null).ToList();
            var movies = _context.Movies.Join(_context.Categories, x => x.CategoryId, y => y.CategoryId, (x, y) => x).ToList();
            //ViewBag.JoinedTable = movies;

            return View(movies);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var recordToEdit = _context.Movies.Single(x => x.MovieID == id);
            ViewBag.Categories = _context.Categories.ToList();

            return View("AddMovie", recordToEdit);
        }
        [HttpPost]
        public IActionResult Edit(NewMovie updatedInfo)
        {

            _context.Update(updatedInfo);
            _context.SaveChanges();

            return RedirectToAction("MovieList");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var recordToDelete = _context.Movies.Single(x => x.MovieID == id);


            return View(recordToDelete);
        }
        [HttpPost]
        public IActionResult Delete(NewMovie movie)
        {
            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return RedirectToAction("MovieList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}