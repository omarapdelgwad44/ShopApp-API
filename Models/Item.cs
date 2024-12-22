using Microsoft.EntityFrameworkCore;
using ShopApp_API.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string ?image { get; set; } // Ensure this is a string
    public bool isFavorite { get; set; }
    public bool isDeleted { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }


}
