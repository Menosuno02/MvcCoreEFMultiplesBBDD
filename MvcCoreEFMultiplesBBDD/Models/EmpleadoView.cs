using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCoreEFMultiplesBBDD.Models
{
    [Table("V_EMPLEADOS")]
    public class EmpleadoView
    {
        [Key]
        [Column("EMP_NO")]
        public int EmpNo { get; set; }
        [Column("APELLIDO")]
        public string Apellido { get; set; }
        [Column("OFICIO")]
        public string Oficio { get; set; }
        [Column("SALARIO")]
        public int Salario { get; set; }
        [Column("DEPT_NO")]
        public int DeptNo { get; set; }
        [Column("DNOMBRE")]
        public string Dnombre { get; set; }
        [Column("LOC")]
        public string Localidad { get; set; }
    }
}
