using Backend_Taller.EFConfiguration;
using Backend_Taller.Models;
using Backend_Taller.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend_Taller.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly TallerDbContext _context;
        public UserRepository(TallerDbContext context)
        {
            _context = context;
        }
        public async Task AgregaClienteAsync(Users user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<Users> ObtenerUserByEmailAsync(string email)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
