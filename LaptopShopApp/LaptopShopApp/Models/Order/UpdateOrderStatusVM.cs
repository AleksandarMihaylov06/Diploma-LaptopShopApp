using LaptopShopApp.Infrastructure.Data.Domain;

namespace LaptopShopApp.Models.Order
{
    public class UpdateOrderStatusVM
    {
       public IEnumerable<OrderIndexVM> Orders { get; set; }
    }
}
