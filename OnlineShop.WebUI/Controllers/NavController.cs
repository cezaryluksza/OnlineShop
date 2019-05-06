using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Domain.Abstract;
using OnlineShop.WebUI.Helpers;
using OnlineShop.WebUI.Models;

namespace OnlineShop.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;

        public NavController(IProductRepository repo)
        {
            repository = repo;
        }
        
        public PartialViewResult Menu(string category = null, SortingType sortingOption = 0)
        { 
            ViewBag.SelectedCategory = category;
            ViewBag.SelectedSortingOption = sortingOption;

            MenuViewModel menuViewModel = new MenuViewModel();

            menuViewModel.Categories = repository.Products.Select(x => x.Category).Distinct().OrderBy(x => x);
            menuViewModel.SortingOptions = Enum.GetValues(typeof(SortingType)).Cast<SortingType>();


            return PartialView("FlexMenu", menuViewModel);
        }


    }
}
