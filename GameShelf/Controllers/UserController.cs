using GameShelf.Data;
using GameShelf.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using GameShelf.Dtos.Responses;

namespace GameShelf.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(AppDbContext db) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (await db.Users.AnyAsync(u => u.Email == request.Email))
                return Conflict("Istnieje już konto z tym adresem email.");

            var user = new User
            {
                Email = request.Email,
                Password = request.Password
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(RegisterRequest request)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user is null || user.Password != request.Password)
                return Unauthorized();

            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserResponse>> GetUserById(int id)
        {
            var user = await db.Users.FindAsync(id);

            if (user is null)
                return NotFound("Tylko że takiego użytkownika nie ma");

            return new UserResponse(user.Id, user.Email, user.Name, user.Role, user.CreatedAt);
        }
    }
}
