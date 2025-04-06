using LaptopShopApp.Infrastructure.Data.Domain;
using System.ComponentModel.DataAnnotations;

namespace LaptopShopApp.Models.Order
{
    public class OrderIndexVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime DateTime { get; set; }
        public string Address { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public List<OrderProductIndexVM> OrderProducts { get; set; }
    }
}
