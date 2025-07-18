﻿using System.ComponentModel.DataAnnotations;

namespace LaptopShopApp.Models.Brand
{
    public class BrandPairVM
    {
        public int Id { get; set; }

        [Display(Name = "Brand")]
        public string Name { get; set; } = null!;
    }
}
