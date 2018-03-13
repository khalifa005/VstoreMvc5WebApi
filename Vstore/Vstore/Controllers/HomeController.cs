using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vstore.Models;
using System.Data.Entity;
namespace Vstore.Controllers
{

  

    [AllowAnonymous]
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;


        public HomeController()
        {
            _context = new ApplicationDbContext();

        }

        public ActionResult Index()
        {

            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}