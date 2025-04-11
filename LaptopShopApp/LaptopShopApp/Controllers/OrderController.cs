using LaptopShopApp.Core.Contracts;
using LaptopShopApp.Infrastructure.Data.Domain;
using LaptopShopApp.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
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
                         DateTime = x.OrderDate,
                         Address = x.Address,
                         OrderId = x.Id,
                         TotalPrice = x.OrderProducts.Select(x => (x.Product.Price - x.Product.Price * x.Product.Discount / 100) * x.Quantity).Sum().ToString("f2"),
                         OrderStatus = x.OrderStatus,
                         OrderProducts = x.OrderProducts.Select(p =>
                             new OrderProductIndexVM()
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.Product.ProductName,
                                 Pic = p.Product.Picture,
                                 Discription = p.Product.Discription,
                                 Quantity = p.Quantity,
                                 Price = (p.Price - p.Product.Price * p.Discount / 100).ToString("f2"),
                             }
                        ).ToList()
                     }
                ).OrderByDescending(x => x.OrderId);

            return View(orderProduct);
        }

        [HttpPost]
        public ActionResult UpdateStatus(UpdateOrderStatusVM vm)
        {
            return RedirectToAction(nameof(Create));
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orderCreate = new OrderCreateVM()
            {
                Address = null,
                
                OrderProducts = _cartService.GetAll(currentUserId)
                .Select(x => new CreateOrderProductVM()
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product.ProductName,
                    Pic = x.Product.Picture,
                    Quantity = x.Quantity,
                    Price = (x.Product.Price - x.Product.Price * x.Product.Discount / 100).ToString("f2"),
                    Discount = x.Product.Discount
                }).ToList()
            };

            orderCreate.TotalPrice = orderCreate.OrderProducts.Select(x => decimal.Parse(x.Price) * x.Quantity).Sum();

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
                        Price = decimal.Parse(x.Price),
                        Discount = x.Discount
                    }).ToList());
                if (created)
                {
                    foreach (var item in orderCreate.OrderProducts)
                    {
                        var product = _productService.GetProductById(item.ProductId);
                        if (product != null)
                        {
                            product.Quantity -= item.Quantity;

                            _productService.UpdateProduct(product);
                        }
                    }
                    _cartService.CleanCart(currentUserId);
                    
                    return RedirectToAction(nameof(MyOrders));
                }
            }
            return View(orderCreate);
        }
        public ActionResult MyOrders()
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orderProduct = _orderService.GetOrdersByUser(currentUserId)
                .Select(x =>
                     new OrderIndexVM()
                     {
                         UserId = x.UserId,
                         UserName = x.User.UserName,
                         DateTime = x.OrderDate,
                         Address = x.Address,
                         OrderId = x.Id,
                         TotalPrice = x.OrderProducts.Select(x => (x.Product.Price - x.Product.Price * x.Product.Discount / 100) * x.Quantity).Sum().ToString("f2"),
                         OrderStatus = x.OrderStatus,
                         OrderProducts = x.OrderProducts.Select(p =>
                             new OrderProductIndexVM()
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.Product.ProductName,
                                 Pic = p.Product.Picture,
                                 Discription = p.Product.Discription,
                                 Quantity = p.Quantity,
                                 Price = (p.Price - p.Product.Price * p.Discount / 100).ToString("f2"),
                             }
                        ).ToList()
                     }
                ).OrderByDescending(x => x.OrderId);
            return View("Index",orderProduct);
        }
    }
}
