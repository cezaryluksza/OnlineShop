using OnlineShop.Domain.Abstract;
using OnlineShop.Domain.Entities;
using OnlineShop.WebUI.Models;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace OnlineShop.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly IOrderProcessor _orderProcessor;


        public CartController(IProductRepository repo, IOrderProcessor proc)
        {
            _repository = repo;
            _orderProcessor = proc;
        }
        [AllowAnonymous]
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                ReturnUrl = returnUrl,
                Cart = cart
            });
        }
        [AllowAnonymous]
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = _repository.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        [AllowAnonymous]
        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = _repository.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        [AllowAnonymous]
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        [AllowAnonymous]
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
        [AllowAnonymous]
        public ViewResult Choice()
        {
            return View(new ShippingDetails());
        }
        [AllowAnonymous]
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (!cart.Lines.Any())
            {
                ModelState.AddModelError("", "Koszyk jest pusty!");
            }
            if (ModelState.IsValid)
            {
                _orderProcessor.ProcessOrder(cart, shippingDetails, null);
                cart.Clear();
                return View("Completed");
            }
            return View(shippingDetails);
        }

        [Authorize(Roles = "Administrator,User")]
        public ViewResult CheckoutLogin()
        {
            var userId = User.Identity.GetUserId();
            Address address = ApplicationUserManager.GetAddressByUserId(userId);
            string name = ApplicationUserManager.GetName(userId);

            var shippingDetails = new ShippingDetails()
            {
                Name = name,
                City = address.City,
                Country = address.Country,
                Line1 = address.Line1,
                Line2 = address.Line2,
                Line3 = address.Line3,
                State = address.State,
                Zip = address.Zip
            };
            
            return View("Checkout", shippingDetails);
        }

        [Authorize(Roles="Administrator,User")]
        [HttpPost]
        public ViewResult CheckoutLogin(Cart cart, ShippingDetails shippingDetails)
        {
            var userId = User.Identity.GetUserId();
            if (!cart.Lines.Any())
            {
                ModelState.AddModelError("", "Koszyk jest pusty!");
            }
            if (ModelState.IsValid)
            {
                _orderProcessor.ProcessOrder(cart, shippingDetails, userId);
                cart.Clear();
                return View("Completed");
            }
            return View("Checkout", shippingDetails);
        }
    }
}