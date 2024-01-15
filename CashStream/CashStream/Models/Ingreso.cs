using CashStream.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashStream.Models
{
    class Ingreso
    {
        public int IdIngreso { get; set; }
        public DateTime Fecha { get; set; }
        public int IdTipoIngreso { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }

        public static bool Agregar(Ingreso ingreso, bool editar)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro("@Fecha", ingreso.Fecha),
                new Parametro("@IdTipoIngreso", ingreso.IdTipoIngreso),
                new Parametro("@Descripcion", ingreso.Descripcion),
                new Parametro("@Monto", ingreso.Monto),
                new Parametro("@Editar", editar),
                new Parametro("@IdIngreso", ingreso.IdIngreso)
             };

            return DbDatos.Ejecutar("Ingreso_Agregar", parametros);
        }
    }
}
