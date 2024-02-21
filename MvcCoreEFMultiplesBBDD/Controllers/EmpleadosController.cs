using Microsoft.AspNetCore.Mvc;
using MvcCoreEFMultiplesBBDD.Models;
using MvcCoreEFMultiplesBBDD.Repositories;

namespace MvcCoreEFMultiplesBBDD.Controllers
{
    public class EmpleadosController : Controller
    {
        private IRepositoryEmpleados repo;

        public EmpleadosController(IRepositoryEmpleados repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<EmpleadoView> empleados = await this.repo.GetEmpleadosAsync();
            return View(empleados);
        }

        public async Task<IActionResult> Details(int id)
        {
            EmpleadoView empleado = await this.repo.FindEmpleadoAsync(id);
            return View(empleado);
        }
    }
}
