using LaptopShopApp.Core.Contracts;
using LaptopShopApp.Core.Services;
using LaptopShopApp.Infrastructure.Data.Domain;
using LaptopShopApp.Models;
using LaptopShopApp.Models.Contact;
using LaptopShopApp.Models.Order;
using LaptopShopApp.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace LaptopShopApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly IMessages _messagesService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, IMessages messagesService)
        {
            _logger = logger;
            _productService = productService;
            _messagesService = messagesService;
        }

        public IActionResult Index()
        {
            var topProducts = _productService.TopSoldProducts(6)
              .Select(x => new ProductIndexVM()
              {
                  Id = x.Id,
                  ProductName = x.ProductName,
                  BrandId = x.BrandId,
                  BrandName = x.Brand.BrandName,
                  CategoryId = x.CategoryId,
                  CategoryName = x.Category.CategoryName,
                  Picture = x.Picture,
                  Discription = x.Discription,
                  Quantity = x.Quantity,
                  Price = x.Price,
                  PriceWithDiscount = (x.Price - x.Price * x.Discount / 100).ToString("f2").ToString(),
                  Discount = x.Discount,
                  HasDiscount = x.Discount != 0,
              }).ToList();
            return View(topProducts);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactVM contactVM)
        {
            if (ModelState.IsValid)
            {
                var created = _messagesService.Create(contactVM.Name, contactVM.Email, contactVM.Message);
                if (created)
                {
                    return View("MessageSend");
                }
            }
            return View("Contact");
        }


        [Authorize(Roles = "Administrator")]
        public IActionResult AllMessages()
        {
            var messages = _messagesService.GetMessages();

            var messageVMs = messages?
                .Select(m => new ContactVM
                {
                    Id = m.Id,
                    Name = m.Name,
                    Email = m.Email,
                    Message = m.Message,
                    SendDate = m.SendDate
                }).ToList() ?? new List<ContactVM>();

            return View("AllMessages", messageVMs);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}