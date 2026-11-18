using GameShelf.Dtos.Requests;
using GameShelf.Dtos.Responses;
using GameShelf.Entities;

namespace GameShelf.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}
