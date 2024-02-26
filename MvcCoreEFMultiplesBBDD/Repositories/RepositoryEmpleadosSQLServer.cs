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

CREATE OR ALTER PROCEDURE SP_DETAILS_EMPLEADO
(@IDEMPLEADO INT)
AS
	SELECT *
	FROM v_empleados
	WHERE EMP_NO = @IDEMPLEADO
GO
*/

#endregion

namespace MvcCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleadosSQLServer : IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleadosSQLServer(HospitalContext context)
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
            var consulta = this.context.EmpleadosView.FromSqlRaw(sql);
            return await consulta.ToListAsync();
        }

        public async Task<EmpleadoView> FindEmpleadoAsync(int idEmpleado)
        {
            /*
            var consulta = from datos in this.context.Empleados
                           where datos.EmpNo == idEmpleado
                           select datos;
            return await consulta.FirstOrDefaultAsync();
            */
            string sql = "SP_DETAILS_EMPLEADO @IDEMPLEADO";
            SqlParameter paramId = new SqlParameter("@IDEMPLEADO", idEmpleado);
            var consulta = this.context.EmpleadosView.FromSqlRaw(sql, paramId);
            EmpleadoView empleado = consulta.AsEnumerable().FirstOrDefault();
            return empleado;
        }
    }
}
