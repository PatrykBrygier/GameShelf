using GameShelf.Dtos;
using GameShelf.Dtos.Requests;
using GameShelf.Dtos.Responses;
using GameShelf.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameShelf.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public Task<AuthResponse> Register(RegisterRequest request) =>
            authService.RegisterAsync(request);

        [HttpPost("login")]
        public Task<AuthResponse> Login(LoginRequest request) =>
            authService.LoginAsync(request);
    }
}