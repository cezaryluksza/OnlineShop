﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Entities
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Required(ErrorMessage = "Proszę podać pierwszy wiersz adresu.")]
        [Display(Name = "Ulica")]
        public string Line1 { get; set; }
        [Display(Name = "Numer domu")]
        public string Line2 { get; set; }
        [Display(Name = "Numer mieszkania")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę miasta.")]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę województwa")]
        [Display(Name = "Województwo")]
        public string State { get; set; }

        [Display(Name = "Kod pocztowy:")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę kraju")]
        [Display(Name = "Kraj:")]
        public string Country { get; set; }


    }
}
