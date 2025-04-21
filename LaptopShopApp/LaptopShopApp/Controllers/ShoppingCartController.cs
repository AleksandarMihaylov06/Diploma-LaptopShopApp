using LaptopShopApp.Core.Contracts;
using LaptopShopApp.Core.Services;
using LaptopShopApp.Data;
using LaptopShopApp.Infrastructure.Data.Domain;
using LaptopShopApp.Models.Product;
using LaptopShopApp.Models.ShoppingCart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Security.Claims;

namespace LaptopShopApp.Controllers
{
    
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _cartService;

        public ShoppingCartController(IShoppingCartService cartService)
        {
            _cartService = cartService;
        }

        // GET: ShoppingCartController
        public ActionResult Index()
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userCart = _cartService.GetAll(currentUserId);
            if(userCart == null || userCart.Count==0)
            {
                return View("EmptyIndex");
            }
            return View(new ShoppingCartIndexVM()
            {
                TotalPrice = userCart.Select(x => (x.Product.Price - x.Product.Price * x.Product.Discount / 100) * x.Quantity).Sum().ToString("f2"),
                UserId = currentUserId,
                Products = userCart.Select(x => new ShoppingCartProductVM()
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product.ProductName,
                    ProductPrice = (x.Product.Price - x.Product.Price * x.Product.Discount / 100).ToString("f2"),
                    HasDiscount = x.Product.Discount != 0,
                    Picture = x.Product.Picture,
                    Quantity = x.Quantity

                }).ToList()
            });
        }

        // POST: ShoppingCartController/Create
        public ActionResult Add(int productId)
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var add = _cartService.AddToCart(currentUserId, productId, 1);
            if (add)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index","Product");
            }
        }

        // GET: ShoppingCartController/Edit/5
        public ActionResult Edit(int productId, int quantity)
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _cartService.ChangeQuantity(currentUserId, productId, quantity);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ClearCart()
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _cartService.CleanCart(currentUserId);
            
            return RedirectToAction(nameof(Index));
        }

        public ActionResult RemoveProduct(int productId)
        {
            if (productId != 0)
            {
                string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var removed = _cartService.RemoveById(currentUserId, productId);

                if (removed)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View("Error");
            }

            return View("Error");
        }
    }
}
