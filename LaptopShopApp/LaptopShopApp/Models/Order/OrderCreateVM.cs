using System.ComponentModel.DataAnnotations;

namespace LaptopShopApp.Models.Order
{
    public class OrderCreateVM
    {
        [Required]
        public string? Address { get; set; }
        public decimal TotalPrice { get; set; }
        public virtual List<CreateOrderProductVM> OrderProducts { get; set; } = new List<CreateOrderProductVM>();
        public bool HasDiscount { get; set; }
    }
}
