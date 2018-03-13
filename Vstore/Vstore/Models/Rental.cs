using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vstore.Models
{
    public class Rental
    {



        public int Id { get; set; }

        public DateTime RentedDate { get; set; }
        public DateTime? DeliveryDate { get; set; }

        [Required]
        public Customer customer { get; set; }

        [Required]
        public Movie Movies { get; set; }
    }
}