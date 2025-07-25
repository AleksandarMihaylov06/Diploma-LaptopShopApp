﻿using System.ComponentModel.DataAnnotations;

namespace LaptopShopApp.Models.Product
{
    public class ProductDeleteVM
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = null!;

        public int BrandId { get; set; }
        [Display(Name = "Brand")]
        public string BrandName { get; set; } = null!;

        public int CategoryId { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; } = null!;

        [Display(Name = "Picture")]
        public string Picture { get; set; } = null!;

        [Display(Name = "Discription")]
        public string Discription { get; set; } = null!;

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }
        public string PriceWithDiscount { get; set; } = null!;

        [Display(Name = "Discount")]
        public decimal Discount { get; set; }
        public bool HasDiscount { get; set; }
    }
}
