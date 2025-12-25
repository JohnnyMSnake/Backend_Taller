using Backend_Taller.DTOs;

namespace Backend_Taller.Services.Interfaces
{
    public interface IPresupuestoService
    {
        Task<bool> VerificarPresupuesto(PresupuestosDTO presupuesto);
    }
}
