using Backend_Taller.DTOs;
using Backend_Taller.EFConfiguration;
using Backend_Taller.Models;
using Backend_Taller.Repository.Interfaces;
using Backend_Taller.Services.Interfaces;
using System.Data;

namespace Backend_Taller.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IHashPasswordService _hashPasswordService;
        private readonly IRoleService _roleService;
        public UserService(IUserRepository userRepository, 
                            IJwtService jwtService, 
                            IHashPasswordService hashPasswordService, 
                            IRoleService roleService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _hashPasswordService = hashPasswordService;
            _roleService = roleService;
        }

        public async Task<UsersResponseDTO> CrearUser(UsersRequestDTO userDTO)
        {
            if (string.IsNullOrWhiteSpace(userDTO.Password) || string.IsNullOrWhiteSpace(userDTO.Email))
            {
                throw new ArgumentException("Se requiere email y password");
            }
            var user = await _userRepository.ObtenerUserByEmailAsync(userDTO.Email);
            if (user is not null)
            {
                throw new ArgumentException("El usuario ya existe, pruebe con otro email");
            }
            
            var role = await _roleService.ObtenerRoleByName("USER");
            await _userRepository.AgregaClienteAsync(new Users
            {
                Email = userDTO.Email,
                Password = _hashPasswordService.HashPassword(userDTO.Password),
                Role = role
            });

            await _userRepository.SaveChangesAsync();
            return new UsersResponseDTO
            {
                Email = userDTO.Email,
            };

        }

        public async Task<string> IniciarSesion(UsersRequestDTO userDTO)
        {
            if (string.IsNullOrWhiteSpace(userDTO.Password) || string.IsNullOrWhiteSpace(userDTO.Email))
            {
                throw new ArgumentException("Se requiere email y password");
            }

            var user = await _userRepository.ObtenerUserByEmailAsync(userDTO.Email);

            if (user is null || !_hashPasswordService.VerifyPassword(userDTO.Password, user.Password))
            {
                throw new ArgumentException("Contraseña o email incorrectos");
            }
            
            return _jwtService.GenerateToken(user);

        }
    }
}
