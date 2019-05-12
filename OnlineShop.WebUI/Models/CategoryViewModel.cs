using OnlineShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.WebUI.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

    }
}