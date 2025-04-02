using System.ComponentModel.DataAnnotations;

namespace LaptopShopApp.Models.ShoppingCart
{
    public class ShoppingCartProductVM
    {
        [Required]
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Picture { get; set; }
        public int Quantity { get; set; }
    }
}
