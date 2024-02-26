using Microsoft.EntityFrameworkCore;
using MvcCoreEFMultiplesBBDD.Data;
using MvcCoreEFMultiplesBBDD.Models;
using MySqlConnector;

#region VIEWS Y PROCEDURES

/*
CREATE OR REPLACE VIEW V_EMPLEADOS
AS
	SELECT IFNULL(EMP.EMP_NO, 0) AS EMP_NO, EMP.APELLIDO, EMP.OFICIO,
	EMP.SALARIO, DEPT.DEPT_NO, DEPT.DNOMBRE, DEPT.LOC
	FROM EMP
	INNER JOIN DEPT
	ON EMP.DEPT_NO = DEPT.DEPT_NO

DELIMITER //
CREATE PROCEDURE SP_ALL_EMPLEADOS()
BEGIN
	SELECT *
    FROM v_empleados;
END //

DELIMITER //
CREATE PROCEDURE SP_DETAILS_EMPLEADO
(IN IDEMPLEADO INT)
BEGIN
	SELECT *
    FROM v_empleados
    WHERE EMP_NO = IDEMPLEADO;
END //
*/

#endregion

namespace MvcCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleadosMySql : IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleadosMySql(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<EmpleadoView>> GetEmpleadosAsync()
        {
            /*
            var consulta = from datos in this.context.EmpleadosView
                           select datos;
            return await consulta.ToListAsync();
            */
            return await this.context.EmpleadosView
                .FromSqlRaw("CALL SP_ALL_EMPLEADOS();").ToListAsync();
        }

        public async Task<EmpleadoView> FindEmpleadoAsync(int idEmpleado)
        {
            /*
            var consulta = from datos in this.context.EmpleadosView
                           where datos.EmpNo == idEmpleado
                           select datos;
            return await consulta.FirstOrDefaultAsync();
            */
            MySqlParameter paramId = new MySqlParameter("IDEMPLEADO", idEmpleado);
            string sql = "CALL SP_DETAILS_EMPLEADO (@IDEMPLEADO);";
            var consulta = this.context.EmpleadosView.FromSqlRaw(sql, paramId);
            return consulta.AsEnumerable().FirstOrDefault();
        }
    }
}
