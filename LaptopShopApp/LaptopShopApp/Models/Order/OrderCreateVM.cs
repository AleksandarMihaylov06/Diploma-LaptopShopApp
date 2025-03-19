using System.ComponentModel.DataAnnotations;

namespace LaptopShopApp.Models.Order
{
    public class OrderCreateVM
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int QuantityInStock { get; set; }
        public string? Picture { get; set; }
        public string Discription { get; set; }

       
    }
}
