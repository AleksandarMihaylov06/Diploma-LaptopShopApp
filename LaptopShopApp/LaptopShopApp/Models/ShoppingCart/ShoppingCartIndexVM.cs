using LaptopShopApp.Infrastructure.Data.Domain;
using System.ComponentModel.DataAnnotations;

namespace LaptopShopApp.Models.ShoppingCart
{
    public class ShoppingCartIndexVM
    {
        [Required]
        public string UserId { get; set; } = null!;
        public string  User { get; set; } = null!;
        [Required]
        public int ProductId { get; set; }
        public string  Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
