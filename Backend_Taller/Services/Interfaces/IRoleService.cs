using Backend_Taller.Models;

namespace Backend_Taller.Services.Interfaces
{
    public interface IRoleService
    {
        Task<Roles> ObtenerRoleByName(string name);
    }
}
