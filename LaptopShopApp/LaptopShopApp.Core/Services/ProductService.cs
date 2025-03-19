using LaptopShopApp.Core.Contracts;
using LaptopShopApp.Data;
using LaptopShopApp.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopShopApp.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(string name, int brandId, int categoryId, string picture, string discription, int quantity, decimal price, decimal discount)
        {
            Product item = new Product
            {
                ProductName = name,
                Brand = _context.Brands.Find(brandId),
                Category = _context.Categories.Find(categoryId),

                Picture = picture,
                Discription = discription,
                Quantity = quantity,
                Price = price,
                Discount = discount
            };

            _context.Products.Add(item);
            return _context.SaveChanges() != 0;
        }

        public Product GetProductById(int productId)
        {
            return _context.Products.Find(productId);
        }

        public List<Product> GetProducts()
        {
            List<Product> products = _context.Products.ToList();
            return products;
        }

        public List<Product> GetProducts(string searchStringCategoryName, string searchStringBrnadName)
        {
            List<Product> products = _context.Products.ToList();
            if (!String.IsNullOrEmpty(searchStringCategoryName) && !String.IsNullOrEmpty(searchStringBrnadName))
            {
                products = products.Where(x => x.Category.CategoryName.ToLower().Contains
                (searchStringCategoryName.ToLower())
                && x.Brand.BrandName.ToLower().Contains(searchStringBrnadName.ToLower())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringCategoryName))
            {
                products = products.Where(x => x.Category.CategoryName.ToLower().Contains(searchStringCategoryName.ToLower())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringBrnadName))
            {
                products = products.Where(x => x.Brand.BrandName.ToLower().Contains(searchStringBrnadName.ToLower())).ToList();
            }
            return products;
        }

        public bool RemoveById(int productId)
        {
            var product = GetProductById(productId);
            if (product == default(Product))
            {
                return false;
            }
            _context.Remove(product);
            return _context.SaveChanges() != 0;
        }

        public bool Update(int productId, string name, int brandId, int categoryId, string picture, string discription, int quantity, decimal price, decimal discount)
        {
            var products = GetProductById(productId);
            if (products == default(Product))
            {
                return false;
            }
            products.ProductName = name;
            //products.BrandId = brandId;
            //products.CategoryId = categoryId;

            products.Brand = _context.Brands.Find(brandId);
            products.Category = _context.Categories.Find(categoryId);

            products.Picture = picture;
            products.Discription = discription;
            products.Quantity = quantity;
            products.Price = price;
            products.Discount = discount;
            _context.Update(products);
            return _context.SaveChanges() != 0;
        }
    }
}