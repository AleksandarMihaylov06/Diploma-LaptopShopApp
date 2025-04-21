using LaptopShopApp.Models.Brand;
using LaptopShopApp.Models.Category;
using System.ComponentModel.DataAnnotations;

namespace LaptopShopApp.Models.Product
{
    public class ProductIndexVM
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        public int BrandId { get; set; }
        [Display(Name = "Brand")]
        public string BrandName { get; set; }
        public int CategoryId { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
        [Display(Name = "Picture")]
        public string Picture { get; set; }
        [Display(Name = "Discription")]
        public string Discription { get; set; }
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        public string PriceWithDiscount { get; set; }
        [Display(Name = "Discount")]
        public decimal Discount { get; set; }
        public bool HasDiscount { get; set; }

        public int SelectedCategoryId { get; set; }
        public int SelectedBrandId { get; set; }
        public IEnumerable<CategoryPairVM> Categories { get; set; }
        public IEnumerable<BrandPairVM> Brands { get; set; }
    }
}
