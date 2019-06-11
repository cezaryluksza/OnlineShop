using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public ShippingDetails ShippingDetails { get; set; }
        
        public ApplicationUser User { get; set; }

        public DateTime? OrderTime { get; set; } 
    }
}
