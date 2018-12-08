using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Domain.Abstract;
using OnlineShop.Domain.Entities;

namespace OnlineShop.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
       
        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ViewResult List() //metoda akcji, która generuje widok pełnej listy produktów
        {
            return View(repository.Products);
        }
    }
}