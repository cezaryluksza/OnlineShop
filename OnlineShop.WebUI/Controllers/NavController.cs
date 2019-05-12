using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Domain.Abstract;
using OnlineShop.Domain.Entities;
using OnlineShop.WebUI.Helpers;
using OnlineShop.WebUI.Models;

namespace OnlineShop.WebUI.Controllers
{
    public class NavController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public NavController(ICategoryRepository categoryRepo)
        {
            _categoryRepository = categoryRepo;
        }
        
        public PartialViewResult Menu(string category, SortingType sortingOption = 0)
        {
            ViewBag.SelectedCategory = category;
            ViewBag.SelectedSortingOption = sortingOption;

            var menuViewModel = new MenuViewModel
            {
                Categories = _categoryRepository.Categories,
                SortingOptions = Enum.GetValues(typeof(SortingType)).Cast<SortingType>()
            };


            return PartialView("FlexMenu", menuViewModel);
        }


    }
}
