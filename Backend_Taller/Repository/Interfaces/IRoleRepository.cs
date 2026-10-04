using Backend_Taller.Models;

namespace Backend_Taller.Repository.Interfaces
{
    public interface IRoleRepository
    {
        Task<Roles> ObtenerRoleByName(string name);
    }
}
