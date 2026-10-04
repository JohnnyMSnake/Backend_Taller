using Backend_Taller.Models;

namespace Backend_Taller.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<Users> ObtenerUserByEmailAsync(string email);
        Task AgregaClienteAsync(Users user);
        Task SaveChangesAsync();
    }
}
