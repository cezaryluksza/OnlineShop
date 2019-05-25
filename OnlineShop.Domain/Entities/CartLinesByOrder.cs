﻿using System.ComponentModel.DataAnnotations;


namespace OnlineShop.Domain.Entities
{
    public class CartLinesByOrder
    {
        [Key]
        public int CartLinesByOrderId { get; set; }

        public CartLine CartLine { get; set; }

        public int OrderId { get; set; }
    }
}
