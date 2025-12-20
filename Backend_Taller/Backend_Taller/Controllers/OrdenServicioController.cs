using Backend_Taller.DTOs;
using Backend_Taller.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Taller.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdenServicioController : ControllerBase
    {
        private readonly TallerDbContext _context;
        public OrdenServicioController(TallerDbContext context)
        {
            _context = context;
        }

        [HttpGet("{Id}")]
        public IActionResult GetOrdenesServicio(int Id)
        {
            var ordenServicio = _context.OrdenesServicio.Where(i => i.OrdenServicioId == Id)
                                                                .Include(v => v.Vehiculo)
                                                                .ThenInclude(m => m.Marca)
                                                                .Include(c => c.Cliente)
                                                                .Include(rfc => rfc.RfcMoral)
                                                                .Include(p => p.Presupuesto)
                                                                .FirstOrDefault();

            if (ordenServicio == null)
            {
                return NotFound();
            }

            return Ok(ordenServicio);
        }

        [HttpPost]
        public IActionResult CreateOrdenServicio([FromBody] CrearOrdenServicioDTO nuevaOrdenServicio)
        {
            //Solo para verificar que si tenga los datos necesarios antes de proceder
            if (nuevaOrdenServicio.Cliente == null || 
                nuevaOrdenServicio.Vehiculo == null || 
                nuevaOrdenServicio.Presupuesto == null)
            {
                return BadRequest("Alguno de los datos a subir no estan completos, favor de no mandar null ya sea el cliente, vehiculo o el presupuesto");
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
                        cliente = _context.Clientes.Find(nuevaOrdenServicio.Cliente.ClientesId);
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
                        _context.Clientes.Add(cliente);

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
                        vehiculo = _context.Vehiculos.Find(nuevaOrdenServicio.Vehiculo.VehiculosId);
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
                        _context.Vehiculos.Add(vehiculo);
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
                            rfcMoral = _context.RfcMorales.Find(nuevaOrdenServicio.RfcMoral.RfcMoralId);
                        }
                        //si no existe, creara uno nuevo
                        if (rfcMoral == null)
                        {
                            rfcMoral = new RfcMoral()
                            {
                                RfcMoralValue = nuevaOrdenServicio.RfcMoral.RfcMoralValue,
                                Institucion = nuevaOrdenServicio.RfcMoral.Institucion
                            };
                            _context.RfcMorales.Add(rfcMoral);

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

                    //Esta parted deberia de crearle un service para que verifique que lo que manda el front tenga sentido 
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

                    _context.Presupuestos.Add(presupuesto);
                    _context.SaveChanges();
                    transaccion.Commit();

                    return Ok(ordenServicio);
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    return StatusCode(500, "Ocurrió un error al crear la orden de servicio.");
                }
            }
        }
    }
}
