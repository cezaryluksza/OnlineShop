using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Entities
{
    public class Addresses
    {
        [Key]
        public int AddressId { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }


    }
}
