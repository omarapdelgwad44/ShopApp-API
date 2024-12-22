namespace ShopApp_API.Dtos
{
    public class ItemDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; } // Accepts the uploaded image file
        public bool isFavorite { get; set; }
        public string CategoryName { get; set; }

    }
}
