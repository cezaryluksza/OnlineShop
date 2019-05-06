using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Domain.Abstract;
using OnlineShop.Domain.Entities;
using OnlineShop.WebUI.Models;

namespace OnlineShop.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 10;

        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }


        public ViewResult List(string category, int page = 1, SortingType sortingOption = 0)
        {
            var products = repository.Products.Where(x => category == null || x.Category == category).OrderBy(x => x.ProductId).Skip((page - 1) * PageSize);

            switch (sortingOption)
            {
                case SortingType.NoSorting:
                    break;
                case SortingType.HighestPrice:
                    products = products.OrderByDescending(x => x.Price);
                        break;
                case SortingType.LowestPrice:
                    products = products.OrderBy(x => x.Price);
                    break;
                case SortingType.MostPopular:
                    products = products.OrderByDescending(x => x.NumberOfBought).ThenByDescending(x => x.Visits);
                    break;
                case SortingType.LargestNumberOfComments:
                    products = products.OrderByDescending(x => x.NumberOfComments).ThenByDescending(x => x.NumberOfBought).ThenByDescending(x => x.Visits).ThenBy(x => x.ProductId);
                    break;
                default:
                    break;

            }
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = products,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category,
                CurrentSorting = sortingOption
            };
            return View(model);
        }

        public FileContentResult GetImage(int productId)
        {
            Product prod = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public ActionResult Product(int productId)
        {
            Product product = repository.Products.FirstOrDefault(x => x.ProductId == productId);
            return View(product);
        }
    }
}