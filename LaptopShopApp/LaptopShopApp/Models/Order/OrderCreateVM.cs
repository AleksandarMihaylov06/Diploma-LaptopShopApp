using System.ComponentModel.DataAnnotations;

namespace LaptopShopApp.Models.Order
{
    public class OrderCreateVM
    {
        public string Address { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public virtual List<CreateOrderProductVM> OrderProducts { get; set; } = new List<CreateOrderProductVM>();
    }
}
