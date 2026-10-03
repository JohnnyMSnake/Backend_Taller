using Backend_Taller.Services.Interfaces;
using BCrypt.Net;

namespace Backend_Taller.Services.Implementations
{
    public class HashPasswordService : IHashPassword
    {
        public string hashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool verifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
