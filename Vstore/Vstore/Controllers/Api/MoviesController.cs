using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vstore.Dtos;
using Vstore.Models;
using System.Data.Entity;

namespace Vstore.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;


        public MoviesController()
        {
            _context = new ApplicationDbContext();
                
        }

        //Get/api/customers
        [HttpGet]
        public IHttpActionResult GetMovies(string query=null)
        {
            var MoviesQuery = _context.Movies.Include(c => c.Genre).Where(c =>c.NumberAvailable>0);

            if (!string.IsNullOrWhiteSpace(query))
                MoviesQuery = MoviesQuery.Where(c => c.Name.Contains(query));



            var MoviesDto = MoviesQuery.ToList().Select(Mapper.Map<Movie, MoviesDto>);
            return Ok(MoviesDto);

        }

        [HttpGet]
        public IHttpActionResult GetMovies(int id) {

            var Movies = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (Movies == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MoviesDto>(Movies));
        }

        [HttpPost]
        public IHttpActionResult AddMovies(MoviesDto MoviesDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var Movies = Mapper.Map<MoviesDto, Movie>(MoviesDto);
            _context.Movies.Add(Movies);


            _context.SaveChanges();
            MoviesDto.Id = Movies.Id;


            return Created(new Uri(Request.RequestUri + "/" + Movies.Id), MoviesDto);

         }

        [HttpPut]
        public IHttpActionResult UpdateMovies(int id,MoviesDto MoviesDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var Movies = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (Movies == null)
                return NotFound();

            Mapper.Map<MoviesDto, Movie>(MoviesDto,Movies);

            _context.SaveChanges();
            //Created(new Uri(Request.RequestUri + "/" + Movies.Id), MoviesDto);

            return Ok();
        }


        [HttpDelete]
        public IHttpActionResult DeleteMovies(int id)
        {
            var Movies = _context.Movies.SingleOrDefault(c => c.Id == id);
            _context.Movies.Remove(Movies);
            _context.SaveChanges();

            return Ok();


        }



    }
}
