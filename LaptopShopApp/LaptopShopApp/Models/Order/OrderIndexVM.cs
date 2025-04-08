using LaptopShopApp.Infrastructure.Data.Domain;
using System.ComponentModel.DataAnnotations;

namespace LaptopShopApp.Models.Order
{
    public class OrderIndexVM
    {
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public string Address { get; set; } = null!;
        public OrderStatus OrderStatus { get; set; }
        public int OrderId { get; set; }
        public List<OrderProductIndexVM> OrderProducts { get; set; }
        public string TotalPrice { get; set; } = null!;
    }
}
