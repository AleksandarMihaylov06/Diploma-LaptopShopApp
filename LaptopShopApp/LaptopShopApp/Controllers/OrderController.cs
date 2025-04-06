using LaptopShopApp.Core.Contracts;
using LaptopShopApp.Core.Services;
using LaptopShopApp.Infrastructure.Data.Domain;
using LaptopShopApp.Models.Brand;
using LaptopShopApp.Models.Category;
using LaptopShopApp.Models.Order;
using LaptopShopApp.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Globalization;
using System.Security.Claims;

namespace LaptopShopApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IShoppingCartService _cartService;

        public OrderController(IProductService productService, IOrderService orderService, IShoppingCartService cartService)
        {
            _productService = productService;
            _orderService = orderService;
            _cartService = cartService;
        }

        // GET: OrderController
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orderProduct = _orderService.GetOrders()
                .Select(x =>
                     new OrderIndexVM()
                     {
                         UserId = x.UserId,
                         UserName = x.User.UserName,
                         DateTime = DateTime.Now,
                         Address = x.Address,
                         OrderStatus = x.OrderStatus,
                         OrderProducts = x.OrderProducts.Select(p =>
                             new OrderProductIndexVM()
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.Product.ProductName,
                                 Pic = p.Product.Picture,
                                 Discription = p.Product.Discription,
                                 Quantity = p.Quantity,
                                 Price = p.Price.ToString(),
                             }
                        ).ToList()
                     }
                );
            return View();
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orderCreate = new OrderCreateVM()
            {
                OrderProducts = _cartService.GetAll(currentUserId)
                .Select(x => new CreateOrderProductVM()
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product.ProductName,
                    Pic = x.Product.Picture,
                    Quantity = x.Quantity,
                    Price = x.Product.Price,
                    Discount = x.Product.Discount
                }).ToList()
            };

            orderCreate.TotalPrice = orderCreate.OrderProducts.Select(x => (x.Price - x.Price * x.Discount / 100) * x.Quantity).Sum();

            return View(orderCreate);
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderCreateVM orderCreate)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var created = _orderService.Create(currentUserId, orderCreate.Address, orderCreate.OrderProducts
                    .Select(x => new OrderProduct()
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Price = x.Price,
                        Discount = x.Discount
                    }).ToList());
                if (created)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction("Create");
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
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

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
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
        public ActionResult Denied()
        {
            return View();
        }
        public ActionResult MyOrders()
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orderProduct = _orderService.GetOrders()
                .Select(x =>
                     new OrderIndexVM()
                     {
                         UserId = x.UserId,
                         UserName = x.User.UserName,
                         DateTime = DateTime.Now,
                         Address = x.Address,
                         OrderStatus = x.OrderStatus,
                         OrderProducts = x.OrderProducts.Select(p =>
                             new OrderProductIndexVM()
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.Product.ProductName,
                                 Pic = p.Product.Picture,
                                 Discription = p.Product.Discription,
                                 Quantity = p.Quantity,
                                 Price = p.Price.ToString(),
                             }
                        ).ToList()
                     }
                );
            return View();
        }
    }
}
