using Backend_Taller.DTOs;
using Backend_Taller.Models;

namespace Backend_Taller.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> IniciarSesion(UsersRequestDTO userDTO);
        Task<UsersResponseDTO> CrearUser(UsersRequestDTO userDTO);
    }
}
