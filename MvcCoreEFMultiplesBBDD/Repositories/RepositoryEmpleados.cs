using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCoreEFMultiplesBBDD.Data;
using MvcCoreEFMultiplesBBDD.Models;

#region VIEWS Y PROCEDIMIENTOS

/*
CREATE OR ALTER VIEW v_empleados
AS
	SELECT ISNULL(EMP.EMP_NO, 0) AS EMP_NO, EMP.APELLIDO, EMP.OFICIO,
	EMP.SALARIO, DEPT.DEPT_NO, DEPT.DNOMBRE, DEPT.LOC
	FROM EMP
	INNER JOIN DEPT
	ON EMP.DEPT_NO = DEPT.DEPT_NO
GO

CREATE OR ALTER PROCEDURE SP_ALL_EMPLEADOS
AS
	SELECT *
	FROM v_empleados
GO
*/

#endregion

namespace MvcCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleados : IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleados(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<EmpleadoView>> GetEmpleadosAsync()
        {
            /*
            var consulta = from datos in this.context.Empleados
                           select datos;
            List<EmpleadoView> empleados = await consulta.ToListAsync();
            return empleados;
            */
            string sql = "SP_ALL_EMPLEADOS";
            var consulta = this.context.Empleados.FromSqlRaw(sql);
            return await consulta.ToListAsync();
        }

        public async Task<EmpleadoView> FindEmpleadoAsync(int idEmpleado)
        {
            var consulta = from datos in this.context.Empleados
                           where datos.EmpNo == idEmpleado
                           select datos;
            return await consulta.FirstOrDefaultAsync();
        }
    }
}
