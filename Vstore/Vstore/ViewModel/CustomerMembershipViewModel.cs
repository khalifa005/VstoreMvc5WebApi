using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vstore.Models;
namespace Vstore.ViewModel
{
    public class CustomerMembershipViewModel
    {
        public Customer Customer { get; set; }
        public IEnumerable<MembershipType> MembershipTypes { get; set; }



    }
}