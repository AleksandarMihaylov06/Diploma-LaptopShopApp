namespace LaptopShopApp.Models.Order
{
    public class OrderProductIndexVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Pic { get; set; }
        public string Discription { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
    }
}
