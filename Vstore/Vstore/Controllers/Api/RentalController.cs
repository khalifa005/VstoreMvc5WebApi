using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vstore.Dtos;
using Vstore.Models;

namespace Vstore.Controllers.Api
{
    public class RentalController : ApiController
    {

        private ApplicationDbContext _context;
        public RentalController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult NewRental(RentalDto rentalDto)
        {
            if (rentalDto.MoviesId.Count == 0)
                return BadRequest("No Movies Is Selected");

            var customer = _context.Customers.Single(c => c.Id == rentalDto.CustomerId);
            if (customer == null)
                return BadRequest("customer id not available");


            var Movies = _context.Movies.Where(m => rentalDto.MoviesId.Contains(m.Id)).ToList();

            if (Movies.Count!= rentalDto.MoviesId.Count)
                return BadRequest("one or more Movies are invalid");

            foreach (var movie in Movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movies is not availabile");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    customer = customer,
                    Movies = movie,
                    RentedDate = DateTime.Now
                };
                _context.Rentals.Add(rental);

            }


            _context.SaveChanges();



            return Ok();
        }




        [HttpPut]
        public IHttpActionResult OldRental(RentalDto rentalDto)
        {
            if (rentalDto.MoviesId.Count == 0)
                return BadRequest("No Movies Is Selected");

            var customer = _context.Customers.Single(c => c.Id == rentalDto.CustomerId);
            if (customer == null)
                return BadRequest("customer id not available");


            var Movies = _context.Movies.Where(m => rentalDto.MoviesId.Contains(m.Id)).ToList();

            if (Movies.Count != rentalDto.MoviesId.Count)
                return BadRequest("one or more Movies are invalid");

            foreach (var movie in Movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movies is not availabile");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    customer = customer,
                    Movies = movie,
                    RentedDate = DateTime.Now
                };
                _context.Rentals.Add(rental);

            }


            _context.SaveChanges();



            return Ok();
        }
    }
}
