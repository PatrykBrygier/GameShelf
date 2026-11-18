using GameShelf.Entities;

namespace GameShelf.Services
{
    public interface ITokenService
    {
        string CreateAccessToken(User user);
    }
}
