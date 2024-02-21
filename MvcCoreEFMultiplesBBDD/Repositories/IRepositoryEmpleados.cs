using MvcCoreEFMultiplesBBDD.Models;

namespace MvcCoreEFMultiplesBBDD.Repositories
{
    public interface IRepositoryEmpleados
    {
        Task<List<EmpleadoView>> GetEmpleadosAsync();
        Task<EmpleadoView> FindEmpleadoAsync(int idEmpleado);
    }
}
