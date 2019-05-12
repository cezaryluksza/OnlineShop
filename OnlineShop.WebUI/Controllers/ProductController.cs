using System.Linq;
using System.Web.Mvc;
using OnlineShop.Domain.Abstract;
using OnlineShop.WebUI.Models;

namespace OnlineShop.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public int PageSize = 10;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepo)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepo;
        }


        public ViewResult List(string category, int page = 1, SortingType sortingOption = 0)
        {


            int categoryId = _categoryRepository.GetCategoryId(category);
            var products = _productRepository.Products.Where(x => category == null || x.CategoryId == categoryId).OrderBy(x => x.ProductId).Skip((page - 1) * PageSize);

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
            }
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
            if (prod.ImageDataThumbnail == null)
            {
                return GetImage(productId);
            }
            return prod != null ? File(prod.ImageDataThumbnail, prod.ImageMimeType) : null;
        }

        public ActionResult Product(int productId)
        {
            var product = _productRepository.Products.FirstOrDefault(x => x.ProductId == productId);
            return View(product);
        }
    }
}