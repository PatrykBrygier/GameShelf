namespace GameShelf.Dtos.Responses
{
    public record UserResponse(int Id, string Email, string Name, string Role, DateTime CreatedAt);
}
