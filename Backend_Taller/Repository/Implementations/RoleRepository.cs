using Backend_Taller.EFConfiguration;
using Backend_Taller.Models;
using Backend_Taller.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend_Taller.Repository.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly TallerDbContext _context;
        public RoleRepository(TallerDbContext context)
        {
            _context = context;
        }
        public async Task<Roles> ObtenerRoleByName(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
        }
    }
}
