using Backend_Taller.DTOs;
using Backend_Taller.Models;
using Backend_Taller.Repository.Interfaces;
using Backend_Taller.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Backend_Taller.Services.Implementations
{
    public class OrdenService : IOrdenService
    {
        private readonly IPresupuestoService _presupuestoService;
        private readonly IOrdenRepository _ordenRepository;
        private readonly IClienteService _clienteService;
        private readonly IVehiculoService _vehiculoService;
        private readonly IRfcMoralService _rfcMoralService;

        public OrdenService(IOrdenRepository ordenRepository, 
                            IPresupuestoService presupuestoService, 
                            IClienteService clienteService, 
                            IVehiculoService vehiculoService,
                            IRfcMoralService rfcMoralService) 
        { 
            _presupuestoService = presupuestoService;
            _ordenRepository = ordenRepository;
            _clienteService = clienteService;
            _vehiculoService = vehiculoService;
            _rfcMoralService = rfcMoralService;
        }

        public async Task<List<OrdenServicio>> BuscarOrden(int? ordenServicioId, string? nombre, string? telefono, string? rfcFisico, string? placas, string? numeroSerie)
        {
            var ordenesServicio = await _ordenRepository.BuscarOrdenAsync(ordenServicioId, nombre, telefono, rfcFisico, placas, numeroSerie);
            return ordenesServicio;
        }

        public async Task<CrearOrdenServicioDTO> CrearOrden(CrearOrdenServicioDTO nuevaOrdenServicio)
        {
            if (nuevaOrdenServicio.Cliente == null ||
                nuevaOrdenServicio.Vehiculo == null ||
                nuevaOrdenServicio.Presupuesto == null)
            {
                throw new ArgumentException("Alguno de los datos a subir no estan completos, favor de no mandar null ya sea el cliente, vehiculo o el presupuesto");
            }

            using (var transaccion = await _ordenRepository.BeginTransactionAsync())
            {
                try
                {
                    
                    OrdenServicio? ordenServicio = null;

                    // Buscar o crear cliente
                    var cliente = await _clienteService.ObtenerOCrearClienteAsync(nuevaOrdenServicio.Cliente);

                    // Buscar o crear vehículo
                    var vehiculo = await _vehiculoService.ObtenerOCrearVehiculoAsync(nuevaOrdenServicio.Vehiculo);

                    // Buscar o crear RFC Moral
                    var rfcMoral = await _rfcMoralService.ObtenerOCrearRfcMoral(nuevaOrdenServicio.RfcMoral);

                    // Crear orden de servicio
                    ordenServicio = new OrdenServicio
                    {
                        Cliente = cliente,
                        Vehiculo = vehiculo,
                        RfcMoral = rfcMoral,
                        Kilometraje = nuevaOrdenServicio.Kilometraje,
                        Observaciones = nuevaOrdenServicio.Observaciones,
                        FechaEntrada = DateTime.Now,
                        Servicios = new List<Servicios>()
                    };

                    foreach (var servicio in nuevaOrdenServicio.Servicios)
                    {
                        var nuevoServicio = new Servicios
                        {
                            Descripcion = servicio.Descripcion,
                            Clave = servicio.Clave,
                            Numero = servicio.Numero
                        };
                        ordenServicio.Servicios.Add(nuevoServicio);
                    }

                    await _ordenRepository.AgregarOrdenServicioAsync(ordenServicio);

                    // Verificar presupuesto
                    var resultado = await _presupuestoService.VerificarPresupuesto(nuevaOrdenServicio.Presupuesto);

                    if (!resultado)
                    {
                        throw new ArgumentException("Verificar el presupuesto, no coincide");
                    }

                    // Crear presupuesto
                    var presupuesto = new Presupuestos()
                    {
                        ManoObra = nuevaOrdenServicio.Presupuesto.ManoObra,
                        Refacciones = nuevaOrdenServicio.Presupuesto.Refacciones,
                        OtrosMateriales = nuevaOrdenServicio.Presupuesto.OtrosMateriales,
                        CargosAdicionales = nuevaOrdenServicio.Presupuesto.CargosAdicionales,
                        Seguro = nuevaOrdenServicio.Presupuesto.Seguro,
                        IVA = nuevaOrdenServicio.Presupuesto.IVA,
                        Subtotal = nuevaOrdenServicio.Presupuesto.Subtotal,
                        Total = nuevaOrdenServicio.Presupuesto.Total,
                        Anticipo = nuevaOrdenServicio.Presupuesto.Anticipo,
                        Resta = nuevaOrdenServicio.Presupuesto.Resta,
                        OrdenServicio = ordenServicio
                    };

                    await _ordenRepository.AgregarPresupuestoAsync(presupuesto);
                    await _ordenRepository.GuardarCambiosAsync();
                    await transaccion.CommitAsync();

                    return nuevaOrdenServicio;
                }
                //NOTA: no se si esta parte deberia de moverla a repository porque tecnicamente es de EF y deberia de estar en repository, pero por el momento lo dejo aqui
                catch (DbUpdateException ex)
                {
                    await transaccion.RollbackAsync();
                    throw;
                }
                catch (ArgumentException)
                {
                    await transaccion.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
