namespace ShopApp_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string? PhoneNumber { get; set; }
        public bool UserType { get; set; } // "Seller" or "Customer"
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}
