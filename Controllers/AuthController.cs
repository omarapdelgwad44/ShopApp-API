using ShopApp_API.Controllers;
using ShopApp_API.Models;
using Microsoft.AspNetCore.Mvc;
using ShopApp_API;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly DataBaseContext _context;

    public AuthController(DataBaseContext context)
    {
        _context = context;
    }


    [HttpPost("register")]
    public IActionResult Register([FromForm] User request)
    {
        // Check if the username already exists
        var existingUser = _context.Users.SingleOrDefault(u => u.Name == request.Name);
        if (existingUser != null)
        {
            return BadRequest("Username already taken");
        }

        // Hash the password
        string hashedPassword = PasswordHelper.HashPassword(request.HashedPassword);

        // Create a new user
        var user = new User
        {
            Name = request.Name,
            HashedPassword = hashedPassword,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            UserType = request.UserType
        };

        // Save the user in the database
        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public IActionResult Login([FromForm] userRequest request)
    {
        var user = _context.Users.SingleOrDefault(u => u.Name == request.Username);

        if (user == null)
        {
            return Unauthorized("Invalid username or password");
        }

        if (!PasswordHelper.VerifyPassword(request.Password, user.HashedPassword))
        {
            return Unauthorized("Invalid username or password");
        }

        // Optionally return JWT token
        return Ok("Login successful");
    }
}
