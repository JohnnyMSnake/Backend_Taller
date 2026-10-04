using System.Globalization;

namespace Backend_Taller.Services.Interfaces
{
    public interface IHashPasswordService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);

    }
}
