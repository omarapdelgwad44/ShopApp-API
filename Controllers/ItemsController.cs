using Microsoft.AspNetCore.Http;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApp_API.Dtos;
using ShopApp_API.Models;
namespace ShopApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly DataBaseContext _db;
        private List<string> _allawedEx = new List<string> { ".jpg", ".png" };
      
        public ItemsController(DataBaseContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var items = await _db.Items.Where(I => !I.isDeleted).Select
            (I => new
            {
                I.Id,
                I.Name,
                I.Description,
                I.Price,
                I.image,
                I.isFavorite,
                CategoryName=  I.Category.Name,
            }
            ).OrderBy(I => I.Name).ToListAsync();
            return Ok(items);
        }

        [HttpGet("favorites")]
        public async Task<IActionResult> GetAllFavoriteAsync()
        {
            var items = await _db.Items.Where(I => !I.isDeleted&&I.isFavorite).Select
             (I => new
             {
                 I.Id,
                 I.Name,
                 I.Description,
                 I.Price,
                 I.image,
                 I.isFavorite
             }
             ).OrderBy(I => I.Name).ToListAsync();
            return Ok(items);
        }

        [HttpPost("addItem")]
        public async Task<IActionResult> AddAsync([FromForm] ItemDto dto)
        {
            if (dto.Price == 0)
            {
                return BadRequest("Prices can't be 0");
            }

            string imgUrl = string.Empty;

            if (dto.ImagePath != null)
            {
                if (!_allawedEx.Contains(Path.GetExtension(dto.ImagePath).ToLower()))
                {
                    return BadRequest("Ex not allowed");
                }

                /*   // Save the uploaded file temporarily
                   var tempFilePath = Path.GetTempFileName();

                   using (var stream = new FileStream(tempFilePath, FileMode.Create))
                   {
                       await dto.ImageFile.CopyToAsync(stream);
                   }
                   // Upload to Cloudinary
                   imgUrl = uploadimage(tempFilePath);

                   // Delete the temporary file after upload
                  System.IO.File.Delete(tempFilePath);
                */
                imgUrl = publicMethods.uploadimage(dto.ImagePath);
            }
            else
            {
                return BadRequest("Image file is required.");
            }
            var CatId = _db.Categories
                           .Where(x => x.Name == dto.CategoryName)
                           .Select(x => x.id)
                           .FirstOrDefault();
            // Create the Item entity
            Item item = new()
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                image = imgUrl,
                isFavorite = dto.isFavorite,
                CategoryId =CatId
            };

            await _db.Items.AddAsync(item);
            await _db.SaveChangesAsync();

            return Ok(item);
        }

        [HttpPut("Setfavorite{id}")]
        public async Task<IActionResult> UpdateFavoriteAsync(int id)
        {
            var item = await _db.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound($"No item with ID {id}");
            }
            {
                item.isFavorite = !item.isFavorite;
            }
            _db.SaveChanges();
            if (item.isFavorite)
            {
                return Ok($"item with ID {id} is now is Favorite");
            }
            else
            {
                return Ok($"item with ID {id} is now not Favorite");
            }
        }
        [HttpDelete("Delete{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var item = await _db.Items.FindAsync(id);
            if (item == null||item.isDeleted)
            {
                return NotFound($"No item with ID {id}");
            }
            {
                item.isDeleted = true;
            }
            _db.SaveChanges();
            
                return Ok($"item with ID {id} is now is Deleted");
        }


    }

}
