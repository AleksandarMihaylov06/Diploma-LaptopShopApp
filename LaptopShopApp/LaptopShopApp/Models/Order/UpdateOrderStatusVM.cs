using LaptopShopApp.Infrastructure.Data.Domain;

namespace LaptopShopApp.Models.Order
{
    public class UpdateOrderStatusVM
    {
        public int OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
