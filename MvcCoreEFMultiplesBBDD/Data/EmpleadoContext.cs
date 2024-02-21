using Microsoft.EntityFrameworkCore;
using MvcCoreEFMultiplesBBDD.Models;

namespace MvcCoreEFMultiplesBBDD.Data
{
    public class EmpleadoContext : DbContext
    {
        public EmpleadoContext(DbContextOptions<EmpleadoContext> options)
            : base(options) { }

        public DbSet<Empleado> Empleados { get; set; }
    }
}
