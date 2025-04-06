using LaptopShopApp.Infrastructure.Data.Domain;
using System.ComponentModel.DataAnnotations;

namespace LaptopShopApp.Models.ShoppingCart
{
    public class ShoppingCartIndexVM
    {
        [Required]
        public string UserId { get; set; } = null!;
        public List<ShoppingCartProductVM> Products { get; set; } = null!;
        public string? TotalPrice { get; set; }
    }
}
