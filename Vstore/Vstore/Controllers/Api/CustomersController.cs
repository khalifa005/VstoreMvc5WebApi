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
    public class CustomersController : ApiController
    {

        private ApplicationDbContext _context;


        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult GetCustomers()
        {

            //deligate fun without call Mapper.Map<Customer,CustomerDto> --()
            return Ok( _context.Customers.Include(c=>c.MembershipType).ToList().Select(Mapper.Map<Customer,CustomerDto>) );
        }



        [HttpGet]

        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();

                return Ok( Mapper.Map<Customer,CustomerDto>(customer));
        }



        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customer.Id = customerDto.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id),customerDto);
        }


        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id,CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var CustomerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (CustomerInDb ==null)
                return NotFound();

                Mapper.Map<CustomerDto, Customer>(customerDto, CustomerInDb);
 
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + CustomerInDb.Id), customerDto);
            
        }



        [HttpDelete]

        public IHttpActionResult DeleteCustomer(int id)
        {
            var CustomerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (!ModelState.IsValid)
                return BadRequest();

            _context.Customers.Remove(CustomerInDb);
            _context.SaveChanges();


            return Ok();

        }


    }
}
