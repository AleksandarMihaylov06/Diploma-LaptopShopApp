using LaptopShopApp.Core.Contracts;
using LaptopShopApp.Data;
using LaptopShopApp.Infrastructure.Data.Domain;
using LaptopShopApp.Models.ShoppingCart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            if(userCart == null)
            {
                return View();
            }
            return View(new ShoppingCartIndexVM()
            {
                UserId = currentUserId,
                Products = userCart.Select(x=> new ShoppingCartProductVM()
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product.ProductName,
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
                return RedirectToAction("Product","Index");
            }
        }

        // GET: ShoppingCartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShoppingCartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingCartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShoppingCartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
