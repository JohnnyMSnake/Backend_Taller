using Backend_Taller.DTOs;
using Backend_Taller.Models;
using Backend_Taller.Repository.Interfaces;
using Backend_Taller.Services.Implementations;
using Moq;

namespace Backend_Taller_Tests.Unit_Test
{
    
    public class PresupuestoServiceTest
    {
        [Fact]
        public async Task VerificarPresupuesto_Valido()
        {
            // Arrange
            var presupuestoRepository = new Mock<IPresupuestoRepository>();

            presupuestoRepository.Setup(repo => repo.ObtenerIvaAsync()).ReturnsAsync(new Iva() { IvaId= 1, IvaValue = 0.16m });



            var presupuestoService = new PresupuestoService(presupuestoRepository.Object);
            var presupuesto = new PresupuestosDTO
            {
                ManoObra = 100,
                Refacciones = 50,
                OtrosMateriales = 20,
                CargosAdicionales = 10,
                Seguro = 5,
                Subtotal = 185, // 100 + 50 + 20 + 10 + 5
                IVA = 29.6m, // asumiendo que IVA es el 16% del subtotal
                Total = 214.6m, // Subtotal + IVA
                Anticipo = 50,
                Resta = 164.6m // Total - Anticipo
            };
            // Act
            var result = await presupuestoService.VerificarPresupuesto(presupuesto);
            // Assert
            Assert.True(result);
        }
        [Fact]
        public async Task VerificarPresupuesto_NoValido()
        {
            // Arrange
            var presupuestoRepository = new Mock<IPresupuestoRepository>();

            presupuestoRepository.Setup(repo => repo.ObtenerIvaAsync()).ReturnsAsync(new Iva() { IvaId = 1, IvaValue = 0.16m });



            var presupuestoService = new PresupuestoService(presupuestoRepository.Object);
            var presupuesto = new PresupuestosDTO
            {
                ManoObra = 100,
                Refacciones = 500,
                OtrosMateriales = 20,
                CargosAdicionales = 19,
                Seguro = 5,
                Subtotal = 185, // 100 + 50 + 20 + 10 + 5
                IVA = 29.6m, // asumiendo que IVA es el 16% del subtotal
                Total = 2114.6m, // Subtotal + IVA
                Anticipo = 50,
                Resta = 164.6m // Total - Anticipo
            };
            // Act
            var result = await presupuestoService.VerificarPresupuesto(presupuesto);
            // Assert
            Assert.False(result);
        }
    }
}
