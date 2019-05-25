using OnlineShop.Domain.Abstract;
using OnlineShop.Domain.Entities;
using OnlineShop.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace OnlineShop.WebUI.Controllers
{
    [AllowAnonymous]
    public class CartController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly IOrderProcessor _orderProcessor;

        public CartController(IProductRepository repo, IOrderProcessor proc)
        {
            _repository = repo;
            _orderProcessor = proc;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                ReturnUrl = returnUrl,
                Cart = cart
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = _repository.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = _repository.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        public ViewResult Choice()
        {
            return View(new ShippingDetails());
        }

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
            else
            {
                return View(shippingDetails);
            }
        }

        public ViewResult CheckoutLogin()
        {
            return View("Checkout", new ShippingDetails());
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
            else
            {
                return View("Checkout", shippingDetails);
            }
        }
    }
}