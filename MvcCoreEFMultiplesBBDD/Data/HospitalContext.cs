using Microsoft.EntityFrameworkCore;
using MvcCoreEFMultiplesBBDD.Models;

namespace MvcCoreEFMultiplesBBDD.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options)
            : base(options) { }

        public DbSet<EmpleadoView> EmpleadosView { get; set; }
    }
}
