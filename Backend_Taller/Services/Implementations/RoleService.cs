using Backend_Taller.Models;
using Backend_Taller.Repository.Implementations;
using Backend_Taller.Repository.Interfaces;
using Backend_Taller.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Backend_Taller.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;

        }
        public async Task<Roles> ObtenerRoleByName(string name)
        {
            return await _roleRepository.ObtenerRoleByName(name);
        }
    }
}
