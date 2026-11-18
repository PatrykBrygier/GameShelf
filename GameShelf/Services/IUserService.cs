using GameShelf.Dtos.Responses;

namespace GameShelf.Services
{
    public interface IUserService
    {
        Task<UserResponse> GetUserByIdAsync(int id);
    }
}
