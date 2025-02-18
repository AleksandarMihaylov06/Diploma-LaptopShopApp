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
    public class BrandServise : IBrandService
    {
        private readonly ApplicationDbContext _context;

        public BrandServise(ApplicationDbContext context)
        {
            _context = context;
        }

        public Brand GetBrandById(int brandId)
        {
            return _context.Brands.Find(brandId);
        }

        public List<Brand> GetBrands()
        {
            List<Brand> brands = _context.Brands.ToList();
            return brands;
        }

        public List<Product> GetProductByBrnad(int brandId)
        {
            return _context.Products
                 .Where(x => x.BrandId == brandId)
                 .ToList();
        }
    }
}
