using Backend_Taller.Models;

namespace Backend_Taller.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
