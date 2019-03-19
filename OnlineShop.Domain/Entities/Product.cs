using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineShop.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductId { get; set; }
        [Display(Name="Nazwa")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText), Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Cena")]
        public decimal Price { get; set; }
        [Display(Name = "Kategoria")]
        public string Category { get; set; }
    }
}
