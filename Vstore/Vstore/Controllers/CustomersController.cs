using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vstore.Models;
using Vstore.ViewModel;
using System.Data.Entity;






namespace Vstore.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ViewResult Index()
        {

            return View();

        }

        public ViewResult Deatils(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            return View(customer);
        }





        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            var VieModel = new CustomerMembershipViewModel
            {

                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", VieModel);
        }

        public ActionResult CustomerForm()
        {
            var memberships = _context.MembershipTypes.ToList();
            var viewModel = new CustomerMembershipViewModel
            {
                MembershipTypes = memberships
            };

            return View(viewModel);
        }



        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult submit(Customer customer)
        {


            if (customer.Id == 0) { _context.Customers.Add(customer); }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.Birthdate = customer.Birthdate;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }





        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewmodel = new CustomerMembershipViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewmodel);
            }

            if (customer.Id == 0) { _context.Customers.Add(customer); }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;

                
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");


        }


















        ////
        






















    }
}