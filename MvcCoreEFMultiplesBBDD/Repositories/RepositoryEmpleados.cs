using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCoreEFMultiplesBBDD.Data;
using MvcCoreEFMultiplesBBDD.Models;

#region PROCEDIMIENTOS ALMACENADOS

/*
CREATE OR ALTER VIEW v_empleados
AS
	SELECT ISNULL(EMP.EMP_NO, 0) AS EMP_NO, EMP.APELLIDO, EMP.OFICIO,
	EMP.SALARIO, DEPT.DEPT_NO, DEPT.DNOMBRE, DEPT.LOC
	FROM EMP
	INNER JOIN DEPT
	ON EMP.DEPT_NO = DEPT.DEPT_NO
GO

CREATE OR ALTER PROCEDURE SP_FIND_VEMPLEADO
(@EMP_NO INT)
AS
	SELECT *
	FROM v_empleados
	WHERE EMP_NO = @EMP_NO
GO
*/

#endregion

namespace MvcCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleados(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            var consulta = from datos in this.context.Empleados
                           select datos;
            List<Empleado> empleados = await consulta.ToListAsync();
            return empleados;
        }

        public async Task<Empleado> FindEmpleadoAsync(int id)
        {
            /*
            string sql = "SP_FIND_VEMPLEADO @EMPNO";
            SqlParameter paramEmpNo = new SqlParameter("@EMPNO", id);
            var consulta = this.context.Empleados.FromSqlRaw(sql, paramEmpNo);
            return await consulta.FirstOrDefaultAsync();
            */
            var consulta = from datos in this.context.Empleados
                           where datos.EmpNo == id
                           select datos;
            return await consulta.FirstOrDefaultAsync();
        }
    }
}
