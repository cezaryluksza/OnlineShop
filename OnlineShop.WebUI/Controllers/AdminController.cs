using System.Drawing;
using OnlineShop.Domain.Abstract;
using OnlineShop.Domain.Entities;
using OnlineShop.WebUI.Helpers;
using OnlineShop.WebUI.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public AdminController(IProductRepository repo, ICategoryRepository categoryRepo)
        {
            _productRepository = repo;
            _categoryRepository = categoryRepo;
        }

        public ViewResult Index()
        {
            return View();
        }

        #region Product
        public ViewResult Products()
        {
            var products = _productRepository.Products;
            foreach (var product in products)
            {
                product.Category = _categoryRepository.Categories.FirstOrDefault(x => x.CategoryId == product.CategoryId);
            }
            return View(_productRepository.Products);
        }
        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        public ViewResult Edit(int productId)
        {
            Product product = _productRepository.Products.FirstOrDefault(p => p.ProductId == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                    product.ImageDataThumbnail = ImageHelper.CreateThumbnail(Image.FromStream(image.InputStream, true, true), 200, 200);
                }
                
                _productRepository.SaveProduct(product);
                TempData["message"] = string.Format($"Zapisano {product.Name} ");
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }


        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deletedProduct = _productRepository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format($"Usunięto {deletedProduct.Name}");
            }
            return RedirectToAction("Index");
        }
        #endregion  

        #region Category

        public ViewResult EditCategory(int categoryId)
        {
            Category category = _categoryRepository.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
            return View(category);
        }
        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.SaveCategory(category);
                TempData["message"] = string.Format($"Zapisano {category.CategoryName} ");
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        public ViewResult Categories()
        {
            return View(_categoryRepository.Categories);
        }

        public ViewResult CreateCategory()
        {
            return View("EditCategory", new Category());
        }

        [ChildActionOnly]
        public ActionResult DropDownList()
        {
            var categories = _categoryRepository.Categories.Distinct();
            var viewmodel = new CategoryViewModel
            {
                Categories = new SelectList(categories, "CategoryId", "CategoryName")
            };

            return PartialView(viewmodel);
        }
        #endregion



    }
}