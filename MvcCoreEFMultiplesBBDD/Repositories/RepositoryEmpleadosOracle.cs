using Microsoft.EntityFrameworkCore;
using MvcCoreEFMultiplesBBDD.Data;
using MvcCoreEFMultiplesBBDD.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

#region VIEWS Y PROCEDIMIENTOS

/*
CREATE OR REPLACE VIEW v_empleados
AS
  SELECT NVL(EMP.EMP_NO, 0) AS EMP_NO, EMP.APELLIDO, EMP.OFICIO,
  EMP.SALARIO, DEPT.DEPT_NO, DEPT.DNOMBRE, DEPT.LOC
  FROM EMP
  INNER JOIN DEPT
  ON EMP.DEPT_NO = DEPT.DEPT_NO;

CREATE OR REPLACE PROCEDURE SP_ALL_EMPLEADOS
(P_CURSOR_EMPLEADOS OUT SYS_REFCURSOR)
AS
BEGIN
  OPEN P_CURSOR_EMPLEADOS FOR
	SELECT *
	FROM v_empleados;
END;
*/

#endregion

namespace MvcCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleadosOracle : IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleadosOracle(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<EmpleadoView>> GetEmpleadosAsync()
        {
            string sql = "begin ";
            sql += "SP_ALL_EMPLEADOS(:P_CURSOR_EMPLEADOS);";
            sql += "end;";
            OracleParameter paramCursor = new OracleParameter();
            paramCursor.ParameterName = "P_CURSOR_EMPLEADOS";
            paramCursor.Value = null;
            paramCursor.Direction = ParameterDirection.Output;
            // Como es un tipo de Oracle, debemos ponerlo de forma manual
            paramCursor.OracleDbType = OracleDbType.RefCursor;
            var consulta = this.context.Empleados.FromSqlRaw(sql, paramCursor);
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
