﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Razor.Generator;

namespace OnlineShop.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę produktu.")]
        [Display(Name="Nazwa")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Proszę podać opis produktu.")]
        [DataType(DataType.MultilineText), Display(Name = "Opis")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Proszę podać dodatnią cenę.")]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Proszę określić kategorię.")]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }

        [Display(Name = "Kategoria")]
        public Category Category { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Visits { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int NumberOfBought { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int NumberOfComments { get; set; }

        public byte[] ImageData { get; set; }
        
        public byte[] ImageDataThumbnail { get; set; }

        public string ImageMimeType { get; set; }

        public bool IsDeleted { get; set; }
    }
}
