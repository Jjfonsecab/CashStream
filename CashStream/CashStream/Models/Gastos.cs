using CashStream.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashStream.Models
{
    class Gastos
    {
        public int IdGasto { get; set; }
        public DateTime Fecha { get; set; }
        public int IdTipoGasto { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }

        public static bool Agregar(Gastos gastos, bool editar)
        { 
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro("@Fecha", gastos.Fecha),
                new Parametro("@IdTipoGasto", gastos.IdTipoGasto),
                new Parametro("@Descripcion", gastos.Descripcion),
                new Parametro("@Monto", gastos.Monto),
                new Parametro("@Editar", editar),
                new Parametro("@IdGasto", gastos.IdGasto)
            };
            return DbDatos.Ejecutar("Gasto_Agregar", parametros);
        }
    }
}
