using Microsoft.AspNetCore.Mvc;
using MvcCoreEFMultiplesBBDD.Models;
using MvcCoreEFMultiplesBBDD.Repositories;

namespace MvcCoreEFMultiplesBBDD.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleados repo;

        public EmpleadosController(RepositoryEmpleados repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Empleado> empleados = await this.repo.GetEmpleados();
            return View(empleados);
        }

        public async Task<IActionResult> Details(int id)
        {
            Empleado empleado = await this.repo.FindEmpleado(id);
            return View(empleado);
        }
    }
}
