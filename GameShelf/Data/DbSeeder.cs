using GameShelf.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext db, IPasswordHasher<User> hasher)
        {
            if (await db.Users.AnyAsync())
                return;

            var users = new[]
            {
                (Email: "jean@claude.vandame", Password: "przysiad17"),
                (Email: "borys@gamil.com", Password: "zupa123"),
                (Email: "ojciec@pijo.com", Password: "piwo525")
            };

            foreach (var (email, password) in users)
            {
                var user = new User { Email = email };
                user.Password = hasher.HashPassword(user, password);
                db.Users.Add(user);
            }

            await db.SaveChangesAsync();
        }
    }
}