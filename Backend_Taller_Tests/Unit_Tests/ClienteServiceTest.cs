
using Backend_Taller.DTOs;
using Backend_Taller.Models;
using Backend_Taller.Repository.Interfaces;
using Backend_Taller.Services.Implementations;
using Moq;

namespace Backend_Taller_Tests.Unit_Test
{
    public class ClienteServiceTest
    {
        [Fact]
        public async Task CreateCliente_ClieteExistente()
        {
            //Arrange
            var clienteRepository = new Mock<IClienteRepository>();

            clienteRepository.Setup(repo => repo.ObtenerClientePorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Clientes { 
                    ClientesId = 1, 
                    Nombre = "Cliente Existente", 
                    Cp = "12345", 
                    Direccion = "Calle Principal 321", 
                    RfcFisico = "RFC123456789", 
                    Telefono = "6142412567" });

            var clienteService = new ClienteService(clienteRepository.Object);

            var clienteDTO = new ClientesDTO
            {
                ClientesId = 1,
                Nombre = "Cliente Existente",
                Cp = "54321",
                Direccion = "Calle Principal 123",
                RfcFisico = "RFC987654321",
                Telefono = "6142412567"
            };

            //Act
            var result = await clienteService.ObtenerOCrearClienteAsync(clienteDTO);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(clienteDTO.Nombre, result.Nombre);
            Assert.Equal(clienteDTO.Cp, result.Cp);
            Assert.Equal(clienteDTO.Direccion, result.Direccion);
            Assert.Equal(clienteDTO.RfcFisico, result.RfcFisico);
            Assert.Equal(clienteDTO.Telefono, result.Telefono);

        }

        [Fact]
        public async Task CreateCliente_ClieteNuevo()
        {
            //Arrange
            var clienteRepository = new Mock<IClienteRepository>();

            clienteRepository.Setup(repo => repo.ObtenerClientePorIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Clientes)null);

            var clienteService = new ClienteService(clienteRepository.Object);

            var clienteDTO = new ClientesDTO
            {
                ClientesId = 0,
                Nombre = "Cliente Nuevo",
                Cp = "54321",
                Direccion = "Calle Principal 123",
                RfcFisico = "RFC987654321",
                Telefono = "6142412567"
            };
            //Act
            var result = await clienteService.ObtenerOCrearClienteAsync(clienteDTO);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(clienteDTO.Nombre, result.Nombre);
            Assert.Equal(clienteDTO.Cp, result.Cp);
            Assert.Equal(clienteDTO.Direccion, result.Direccion);
            Assert.Equal(clienteDTO.RfcFisico, result.RfcFisico);
            Assert.Equal(clienteDTO.Telefono, result.Telefono);
        }

        [Fact]
        public async Task CreateCliente_NullDTO_ThrowsArgumentNullException()
        {
            //Arrange
            var clienteRepository = new Mock<IClienteRepository>();
            var clienteService = new ClienteService(clienteRepository.Object);
            //Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => clienteService.ObtenerOCrearClienteAsync(null));
        }
    }
}
