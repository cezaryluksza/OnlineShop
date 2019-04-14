using OnlineShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.WebUI.Models
{
    public class MenuViewModel
    {
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<SortingType> SortingOptions { get; set; }
    }
}