using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApp_API.Dtos;
using ShopApp_API.Models;


namespace ShopApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly DataBaseContext _db;
        private List<string> _allawedEx = new List<string> { ".jpg", ".png" };

        public CategoriesController(DataBaseContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var items = await _db.Categories.Select
            (I => new
            {
                I.id,
                I.Name,
                I.image,
                I.Items,
                
            }
            ).OrderBy(I => I.Name).ToListAsync();
            return Ok(items);
        }
        [HttpPost("addCategory")]
        public async Task<IActionResult> AddAsync([FromForm] CategoriesDto dto)
        {
          

            string imgUrl = string.Empty;
            if (dto.image != null)
            {
                if (!_allawedEx.Contains(Path.GetExtension(dto.image).ToLower()))
                {
                    return BadRequest("Ex not allowed");
                }
                imgUrl =publicMethods.uploadimage(dto.image);
            }
            else
            {
                return BadRequest("Image file is required.");
            }
           // Create the cat entity
            Category category = new()
            {
                Name = dto.name,
                image = imgUrl,
            };

            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();

            return Ok(category);
        }

    }
}
