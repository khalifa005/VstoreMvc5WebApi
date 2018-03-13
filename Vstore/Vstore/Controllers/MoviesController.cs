using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vstore.ViewModel;
using Vstore.Models;
using System.Data.Entity;
using System.IO;

namespace Vstore.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: Movies
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanMangeMovies))
                return View("List");

            return View("ReadOnlyList");

        }


        public ViewResult New()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieGenreViewModel
            {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }


        public ViewResult Deatils(int id)
        {
            var MO = _context.Movies.SingleOrDefault(c=>c.Id==id);

            return View(MO);
        }

        //[Authorize(Roles = RoleName.CanMangeMovies)]
        //public ViewResult MoviesForm()
        //{
        //    var ViewModel = new MoviesGenreViewModel
        //    {
        //        Genres = _context.Genres.ToList()
        //    };

        //    return View(ViewModel);
        //}



        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            var ViewModel = new MovieGenreViewModel(movie)
            {
                
                Genres = _context.Genres.ToList()
            };




            return View("MovieForm",ViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieGenreViewModel(movie)
                {
             
                Genres = _context.Genres.ToList()

                };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                //string fileName = Path.GetFileNameWithoutExtension(movie.ImageFile.FileName);
                //string extension = Path.GetExtension(movie.ImageFile.FileName);
                //fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                //movie.ImagePath = "~/Image/" + fileName;
                //fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                //movie.ImageFile.SaveAs(fileName);



            


                movie.DateAdded = DateTime.Now;
                
                movie.NumberAvailable = movie.NumberInStock;

                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                //movieInDb.ImagePath = movie.ImagePath;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }












    }
}