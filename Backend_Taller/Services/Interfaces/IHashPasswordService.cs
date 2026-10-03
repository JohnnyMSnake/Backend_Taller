using System.Globalization;

namespace Backend_Taller.Services.Interfaces
{
    public interface IHashPassword
    {
        string hashPassword(string password);
        bool verifyPassword(string password, string hashedPassword);

    }
}
