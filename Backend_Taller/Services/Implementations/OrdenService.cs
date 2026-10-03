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

        public OrdenService(IOrdenRepository ordenRepository, IPresupuestoService presupuestoService) 
        { 
            _presupuestoService = presupuestoService;
            _ordenRepository = ordenRepository;
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
                throw new BadHttpRequestException("Alguno de los datos a subir no estan completos, favor de no mandar null ya sea el cliente, vehiculo o el presupuesto");
            }

            using (var transaccion = await _ordenRepository.BeginTransactionAsync())
            {
                try
                {
                    Clientes? cliente = null;
                    Vehiculos? vehiculo = null;
                    RfcMoral? rfcMoral = null;
                    OrdenServicio? ordenServicio = null;

                    // Buscar o crear cliente
                    if (nuevaOrdenServicio.Cliente.ClientesId > 0)
                    {
                        cliente = await _ordenRepository.ObtenerClientePorIdAsync(nuevaOrdenServicio.Cliente.ClientesId);
                    }

                    if (cliente == null)
                    {
                        cliente = new Clientes
                        {
                            RfcFisico = nuevaOrdenServicio.Cliente.RfcFisico,
                            Nombre = nuevaOrdenServicio.Cliente.Nombre,
                            Direccion = nuevaOrdenServicio.Cliente.Direccion,
                            Cp = nuevaOrdenServicio.Cliente.Cp,
                            Telefono = nuevaOrdenServicio.Cliente.Telefono
                        };
                        await _ordenRepository.AgregarClienteAsync(cliente);
                    }
                    else
                    {
                        cliente.RfcFisico = nuevaOrdenServicio.Cliente.RfcFisico;
                        cliente.Nombre = nuevaOrdenServicio.Cliente.Nombre;
                        cliente.Direccion = nuevaOrdenServicio.Cliente.Direccion;
                        cliente.Cp = nuevaOrdenServicio.Cliente.Cp;
                        cliente.Telefono = nuevaOrdenServicio.Cliente.Telefono;
                    }

                    // Buscar o crear vehículo
                    if (nuevaOrdenServicio.Vehiculo.VehiculosId > 0)
                    {
                        vehiculo = await _ordenRepository.ObtenerVehiculoPorIdAsync(nuevaOrdenServicio.Vehiculo.VehiculosId);
                    }

                    if (vehiculo == null)
                    {
                        vehiculo = new Vehiculos
                        {
                            NumeroSerie = nuevaOrdenServicio.Vehiculo.NumeroSerie,
                            Placas = nuevaOrdenServicio.Vehiculo.Placas,
                            Tipo = nuevaOrdenServicio.Vehiculo.Tipo,
                            MarcasId = nuevaOrdenServicio.Vehiculo.MarcasId,
                            Modelo = nuevaOrdenServicio.Vehiculo.Modelo,
                            NumeroMotor = nuevaOrdenServicio.Vehiculo.NumeroMotor,
                            Color = nuevaOrdenServicio.Vehiculo.Color
                        };
                        await _ordenRepository.AgregarVehiculoAsync(vehiculo);
                    }
                    else
                    {
                        vehiculo.NumeroSerie = nuevaOrdenServicio.Vehiculo.NumeroSerie;
                        vehiculo.Placas = nuevaOrdenServicio.Vehiculo.Placas;
                        vehiculo.Tipo = nuevaOrdenServicio.Vehiculo.Tipo;
                        vehiculo.MarcasId = nuevaOrdenServicio.Vehiculo.MarcasId;
                        vehiculo.Modelo = nuevaOrdenServicio.Vehiculo.Modelo;
                        vehiculo.NumeroMotor = nuevaOrdenServicio.Vehiculo.NumeroMotor;
                        vehiculo.Color = nuevaOrdenServicio.Vehiculo.Color;
                    }

                    // Buscar o crear RFC Moral
                    if (nuevaOrdenServicio.RfcMoral != null)
                    {
                        if (nuevaOrdenServicio.RfcMoral.RfcMoralId > 0)
                        {
                            rfcMoral = await _ordenRepository.ObtenerRfcMoralPorIdAsync(nuevaOrdenServicio.RfcMoral.RfcMoralId);
                        }

                        if (rfcMoral == null)
                        {
                            rfcMoral = new RfcMoral()
                            {
                                RfcMoralValue = nuevaOrdenServicio.RfcMoral.RfcMoralValue,
                                Institucion = nuevaOrdenServicio.RfcMoral.Institucion
                            };
                            await _ordenRepository.AgregarRfcMoralAsync(rfcMoral);
                        }
                        else
                        {
                            rfcMoral.RfcMoralValue = nuevaOrdenServicio.RfcMoral.RfcMoralValue;
                            rfcMoral.Institucion = nuevaOrdenServicio.RfcMoral.Institucion;
                        }
                    }

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
