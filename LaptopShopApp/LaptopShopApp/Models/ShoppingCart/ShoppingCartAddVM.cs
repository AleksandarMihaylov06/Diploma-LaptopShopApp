using MessagePack.Formatters;

namespace LaptopShopApp.Models.ShoppingCart
{
    public class ShoppingCartAddVM
    {
        public string UserId { get; set; } = null!;
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
