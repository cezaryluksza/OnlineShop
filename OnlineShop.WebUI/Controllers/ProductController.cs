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
        public int PageSize = 4;

        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ViewResult List(int page = 1) //metoda akcji, która generuje widok pełnej listy produktów
        {
            return View(repository.Products.OrderBy(p => p.ProductId).Skip((page - 1) * PageSize).Take(PageSize));
        }
    }
}