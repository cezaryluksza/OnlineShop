using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineShop.Domain.Entities
{
    public class Category
    {
        [HiddenInput(DisplayValue = false)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę kategorii.")]
        [Display(Name = "Kategoria")]
        public string CategoryName { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType()) return false;

            Category p = (Category)obj;
            return (this.CategoryName == p.CategoryName);
        }

        public override int GetHashCode()
        {
            return this.CategoryId.GetHashCode();
        }

        public static bool operator ==(Category category1, Category category2)
        {
            if (System.Object.ReferenceEquals(category1, category2))
            {
                return true;
            }

            if (((object)category1 == null) || ((object)category2 == null))
            {
                return false;
            }

            return category1.CategoryName == category2.CategoryName;
        }

        public static bool operator !=(Category category1, Category category2)
        {
            return !(category1 == category2);
        }
    }
}
