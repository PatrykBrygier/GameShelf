using GameShelf.Common;
using GameShelf.Data;
using GameShelf.Dtos;
using GameShelf.Dtos.Requests;
using GameShelf.Dtos.Responses;
using GameShelf.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Services
{
    public class AuthService(
        AppDbContext db,
        IPasswordHasher<User> hasher,
        ITokenService tokenService) : IAuthService
    {
        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var email = NormalizeEmail(request.Email);

            if (await db.Users.AnyAsync(u => u.Email == email))
                throw new ApiException(StatusCodes.Status409Conflict, "Email jest już zajęty.");

            var user = new User { Email = email };
            user.Password = hasher.HashPassword(user, request.Password);

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return BuildAuthResponseWithToken(user);
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var email = NormalizeEmail(request.Email);
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user is null ||
                hasher.VerifyHashedPassword(user, user.Password, request.Password)
                    == PasswordVerificationResult.Failed)
                throw new ApiException(StatusCodes.Status401Unauthorized, "Nieprawidłowy email lub hasło.");

            return BuildAuthResponseWithToken(user);
        }

        private AuthResponse BuildAuthResponseWithToken(User user) =>
            new(tokenService.CreateAccessToken(user), new UserResponse(user.Id, user.Email, user.Name, user.Role, user.CreatedAt));

        private static string NormalizeEmail(string email) => email.Trim().ToLowerInvariant();

    }
}