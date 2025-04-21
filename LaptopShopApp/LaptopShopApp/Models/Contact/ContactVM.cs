namespace LaptopShopApp.Models.Contact
{
    public class ContactVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime SendDate { get; set; }
    }
}
