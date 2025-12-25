using Backend_Taller.DTOs;
using Backend_Taller.Models;
using Backend_Taller.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend_Taller.Services.Implementations
{
    public class CrearOrdenService : ICrearOrdenService
    {
        private readonly IPresupuestoService _presupuestoService;
        private readonly TallerDbContext _context;
        public CrearOrdenService(TallerDbContext context, IPresupuestoService presupuestoSevice) 
        { 
            _presupuestoService = presupuestoSevice;
            _context = context;
        }
        public async Task<CrearOrdenServicioDTO> CrearOrden(CrearOrdenServicioDTO nuevaOrdenServicio)
        {
            //Solo para verificar que si tenga los datos necesarios antes de proceder
            if (nuevaOrdenServicio.Cliente == null ||
                nuevaOrdenServicio.Vehiculo == null ||
                nuevaOrdenServicio.Presupuesto == null)
            {
                throw new BadHttpRequestException("Alguno de los datos a subir no estan completos, favor de no mandar null ya sea el cliente, vehiculo o el presupuesto");
            }

            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {
                    Clientes? cliente = null;
                    Vehiculos? vehiculo = null;
                    RfcMoral? rfcMoral = null;
                    OrdenServicio? ordenServicio = null;

                    //Primero buscara si el cliente ya existe
                    if (nuevaOrdenServicio.Cliente.ClientesId > 0)
                    {
                        cliente = await _context.Clientes.FindAsync(nuevaOrdenServicio.Cliente.ClientesId);
                    }
                    //este creara un nuevo cliente si no existe
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
                        await _context.Clientes.AddAsync(cliente);

                    }
                    //si el cliente ya existe, actualizara su informacion
                    else
                    {
                        cliente.RfcFisico = nuevaOrdenServicio.Cliente.RfcFisico;
                        cliente.Nombre = nuevaOrdenServicio.Cliente.Nombre;
                        cliente.Direccion = nuevaOrdenServicio.Cliente.Direccion;
                        cliente.Cp = nuevaOrdenServicio.Cliente.Cp;
                        cliente.Telefono = nuevaOrdenServicio.Cliente.Telefono;

                    }
                    //Igual que con el usuario, primero buscara si el vehiculo ya existe
                    if (nuevaOrdenServicio.Vehiculo.VehiculosId > 0)
                    {
                        vehiculo = await _context.Vehiculos.FindAsync(nuevaOrdenServicio.Vehiculo.VehiculosId);
                    }
                    //si no existe, creara uno nuevo
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
                        await _context.Vehiculos.AddAsync(vehiculo);
                    }
                    //si ya existe, actualizara su informacion
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
                    if (nuevaOrdenServicio.RfcMoral != null)
                    {
                        //Finalmente, hara lo mismo para el RFC Moral
                        if (nuevaOrdenServicio.RfcMoral.RfcMoralId > 0)
                        {
                            rfcMoral = await _context.RfcMorales.FindAsync(nuevaOrdenServicio.RfcMoral.RfcMoralId);
                        }
                        //si no existe, creara uno nuevo
                        if (rfcMoral == null)
                        {
                            rfcMoral = new RfcMoral()
                            {
                                RfcMoralValue = nuevaOrdenServicio.RfcMoral.RfcMoralValue,
                                Institucion = nuevaOrdenServicio.RfcMoral.Institucion
                            };
                            await _context.RfcMorales.AddAsync(rfcMoral);

                        }
                        //si ya existe, actualizara su informacion
                        else
                        {
                            rfcMoral.RfcMoralValue = nuevaOrdenServicio.RfcMoral.RfcMoralValue;
                            rfcMoral.Institucion = nuevaOrdenServicio.RfcMoral.Institucion;
                        }

                    }

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
                    _context.OrdenesServicio.Add(ordenServicio);

                    var resultado = await _presupuestoService.VerificarPresupuesto(nuevaOrdenServicio.Presupuesto);

                    if (!resultado)
                    {
                        throw new ArgumentException("Verificar el presupuesto, no coincide");
                    }

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

                    await _context.Presupuestos.AddAsync(presupuesto);
                    await _context.SaveChangesAsync();
                    await transaccion.CommitAsync();

                    return nuevaOrdenServicio;
                }
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
