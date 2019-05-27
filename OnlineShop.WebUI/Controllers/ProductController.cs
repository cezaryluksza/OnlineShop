using System.Collections.Generic;
using System.Linq;
using System.Net;
using System;
using System.Web.Mvc;
using OnlineShop.Domain.Abstract;
using OnlineShop.Domain.Entities;
using OnlineShop.WebUI.Models;

namespace OnlineShop.WebUI.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public int PageSize = 16;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepo)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepo;
        }


        public ViewResult List(string category, int page = 1, SortingType sortingOption = 0)
        {
            int categoryId = _categoryRepository.GetCategoryId(category);
            IEnumerable<Product> products = _productRepository.Products.OrderBy(x => x.ProductId);

            switch (sortingOption)
            {
                case SortingType.NoSorting:
                    break;
                case SortingType.NameAZ:
                    products = products.OrderBy(x => x.Name);
                    break;
                case SortingType.NameZA:
                    products = products.OrderByDescending(x => x.Name);
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
            }

            if (page == 1)
                products = products.Where(x => category == null || x.CategoryId == categoryId).Take(PageSize);
            else
                products = products.Where(x => category == null || x.CategoryId == categoryId).Skip((page - 1) * PageSize);

            var model = new ProductsListViewModel
            {
                Products = products,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? _productRepository.Products.Count() : _productRepository.Products.Count(e => e.CategoryId == categoryId)
                },
                CurrentCategory = category,
                CurrentSorting = sortingOption
            };
            return View(model);
        }

        public FileContentResult GetImage(int productId)
        {
            var prod = _productRepository.Products.FirstOrDefault(p => p.ProductId == productId);
            return prod != null ? File(prod.ImageData, prod.ImageMimeType) : null;
        }
        public FileContentResult GetImageThumbnail(int productId)
        {
            var prod = _productRepository.Products.First(p => p.ProductId == productId);
            if (prod?.ImageDataThumbnail == null)
            {
                return GetImage(productId);
            }
            return prod != null ? File(prod.ImageDataThumbnail, prod.ImageMimeType) : null;
        }

        public ActionResult Product(int productId)
        {
            var product = _productRepository.Products.FirstOrDefault(x => x.ProductId == productId);
            product.Category = _categoryRepository.Categories.FirstOrDefault(x => x.CategoryId == product.CategoryId);

            var visited = Convert.ToBoolean(System.Web.HttpContext.Current.Session["VisitedId: " + productId]);
            if (!visited)
            {
                System.Web.HttpContext.Current.Session["VisitedId: " + productId] = true;
                product.Visits++;
                _productRepository.SaveProduct(product);
            }
            return View(product);
        }
    }
}