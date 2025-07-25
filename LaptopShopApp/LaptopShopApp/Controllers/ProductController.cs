﻿using LaptopShopApp.Core.Contracts;
using LaptopShopApp.Infrastructure.Data.Domain;
using LaptopShopApp.Models.Brand;
using LaptopShopApp.Models.Category;
using LaptopShopApp.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaptopShopApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;

        public ProductController(IProductService productService, ICategoryService categoryService, IBrandService brandService)
        {
            this._productService = productService;
            this._categoryService = categoryService;
            this._brandService = brandService;
        }

        [AllowAnonymous]
        //GET: ProductController
        public ActionResult Index(string searchStringCategoryName, string searchStringBrandName, string searchStringProductName, string hasDiscount)
        {
            
            List<ProductIndexVM> products = _productService.GetProducts(searchStringCategoryName, searchStringBrandName, searchStringProductName, hasDiscount == null ? false : true)
                .Select(product => new ProductIndexVM
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    BrandId = product.BrandId,
                    BrandName = product.Brand.BrandName,
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category.CategoryName,
                    Picture = product.Picture,
                    Discription = product.Discription,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    PriceWithDiscount = (product.Price - product.Price * product.Discount / 100).ToString("f2").ToString(),
                    Discount = product.Discount,
                    HasDiscount = product.Discount != 0,
                }).ToList();
            
            
            ViewBag.Categories = _categoryService.GetCategories()
       .Select(c => new CategoryPairVM { Id = c.Id, Name = c.CategoryName })
       .ToList();

            ViewBag.Brands = _brandService.GetBrands()
                .Select(b => new BrandPairVM { Id = b.Id, Name = b.BrandName })
                .ToList();

            ViewBag.SelectedCategory = searchStringCategoryName;
            ViewBag.SelectedBrand = searchStringBrandName;
            ViewBag.SearchStringProductName = searchStringProductName;

            return this.View(products);
        }

        [AllowAnonymous]
        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            Product item = _productService.GetProductById(id);
            if (item == null)
            {
                return NotFound();
            }
            ProductDetailsVM product = new ProductDetailsVM()
            {
                Id = item.Id,
                ProductName = item.ProductName,
                BrandId = item.BrandId,
                BrandName = item.Brand.BrandName,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.CategoryName,
                Picture = item.Picture,
                Discription = item.Discription,
                Quantity = item.Quantity,
                Price = item.Price.ToString(),
                PriceWithDiscount = (item.Price - item.Price * item.Discount / 100).ToString("f2").ToString(),
                Discount = item.Discount.ToString(),
                HasDiscount = item.Discount != 0,
            };
            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            var product = new ProductCreateVM();
            product.Brands = _brandService.GetBrands()
                .Select(x => new BrandPairVM()
                {
                    Id = x.Id,
                    Name = x.BrandName
                }).ToList();
            product.Categories = _categoryService.GetCategories()
               .Select(x => new CategoryPairVM()
               {
                   Id = x.Id,
                   Name = x.CategoryName
               }).ToList();
            return View(product);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] ProductCreateVM product)
        {
            if (ModelState.IsValid)
            {
                var createdId = _productService.Create(product.ProductName, product.BrandId, product.CategoryId,
                    product.Picture,product.Discription, product.Quantity, product.Price, product.Discount);
                if (createdId)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction("Create");
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            ProductEditVM updatedProduct = new ProductEditVM()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Picture = product.Picture,
                Discription = product.Discription,
                Quantity = product.Quantity,
                Price = product.Price,
                Discount = product.Discount
            };
            updatedProduct.Brands = _brandService.GetBrands()
                .Select(b => new BrandPairVM()
                {
                    Id = b.Id,
                    Name = b.BrandName
                })
                .ToList();
            updatedProduct.Categories = _categoryService.GetCategories()
                .Select(c => new CategoryPairVM()
                {
                    Id = c.Id,
                    Name = c.CategoryName
                })
                .ToList();
            return View(updatedProduct);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductEditVM product)
        {
            if (ModelState.IsValid)
            {
                var updated = _productService.Update(id, product.ProductName, product.BrandId,
                    product.CategoryId, product.Picture,  product.Discription,
                    product.Quantity, product.Price, product.Discount);
                if (updated)
                {
                    return this.RedirectToAction("Index");
                }
            }
            return View(product);
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            Product item = _productService.GetProductById(id);
            if (item == null)
            {
                return NotFound();
            }
            ProductDeleteVM product = new ProductDeleteVM()
            {
                Id = item.Id,
                ProductName = item.ProductName,
                BrandId = item.BrandId,
                BrandName = item.Brand.BrandName,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.CategoryName,
                Picture = item.Picture,
                Discription = item.Discription,
                Quantity = item.Quantity,
                Price = item.Price,
                PriceWithDiscount = (item.Price - item.Price * item.Discount / 100).ToString("f2").ToString(),
                Discount = item.Discount,
                HasDiscount = item.Discount != 0, 
            };
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = _productService.RemoveById(id);
            if (deleted)
            {
                return this.RedirectToAction("Success");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
