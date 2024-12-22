using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShopApp_API.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category
    {
      public int id { get; set;}
     [Required]
      public  string Name { get; set; }
      public string image { get; set; }
      public ICollection<Item> Items { get; set; }
        
    }
}
