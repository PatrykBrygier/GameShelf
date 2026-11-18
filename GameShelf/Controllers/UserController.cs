using GameShelf.Dtos;
using GameShelf.Dtos.Responses;
using GameShelf.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameShelf.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/users")]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpGet("me")]
        public Task<UserResponse> GetCurrentUser()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            return userService.GetUserByIdAsync(id);
        }

        [HttpGet("{id:int}")]
        public Task<UserResponse> GetUserById(int id) =>
            userService.GetUserByIdAsync(id);
    }
}