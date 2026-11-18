using GameShelf.Common;
using GameShelf.Data;
using GameShelf.Dtos;
using GameShelf.Dtos.Responses;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Services
{
    public class UserService(AppDbContext db) : IUserService
    {
        public async Task<UserResponse> GetUserByIdAsync(int id)
        {
            var user = await db.Users
                .Where(u => u.Id == id)
                .Select(u => new UserResponse(u.Id, u.Email, u.Name, u.Role, u.CreatedAt))
                .FirstOrDefaultAsync();

            return user ?? throw new ApiException(StatusCodes.Status404NotFound, "Użytkownik nie istnieje.");
        }
    }
}