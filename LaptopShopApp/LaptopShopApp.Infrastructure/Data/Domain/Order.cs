 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopShopApp.Infrastructure.Data.Domain
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string UserId { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
        public string Address { get; set; } = null!;
        public OrderStatus OrderStatus { get; set; }
        
        public virtual IEnumerable<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
